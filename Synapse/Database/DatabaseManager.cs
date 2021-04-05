using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using LiteDB;
using Swan;
using Synapse.Api;

namespace Synapse.Database
{
    public class DatabaseManager
    {
        public static PlayerRepository PlayerRepository = new PlayerRepository();
        public static PunishmentRepository PunishmentRepository = new PunishmentRepository();

        public static LiteDatabase LiteDatabase =>
            !SynapseController.Server.Configs.synapseConfiguration.DatabaseEnabled
                ? null
                : new LiteDatabase(Path.Combine(SynapseController.Server.Files.DatabaseDirectory,
                    SynapseController.Server.Configs.synapseConfiguration.DatabaseShared
                        ? "database.db"
                        : $"server-{ServerStatic.ServerPort}.db"));

        public static void CheckEnabledOrThrow()
        {
            if (!SynapseController.Server.Configs.synapseConfiguration.DatabaseEnabled)
                throw new DataException("The Database has been disabled in the config. " +
                                        "Please check SynapseController.EnableDatabase before accessing connected APIs");
        }
    }

    public class PlayerRepository : Repository<PlayerDbo>
    {
        public PlayerDbo FindByGameId(string id)
        {
            return Get(LiteDB.Query.EQ("GameIdentifier", id));
        }

        public bool ExistGameId(string id)
        {
            return Exists(LiteDB.Query.EQ("GameIdentifier", id));
        }
    }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class PlayerDbo : IDatabaseEntity
    {
        public int Id { get; set; }

        public string GameIdentifier { get; set; }

        public string Name { get; set; }

        public bool DoNotTrack { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public int GetId()
        {
            return Id;
        }
    }


    public class PunishmentRepository : Repository<PunishmentDbo>
    {
        public List<PunishmentDbo> GetCurrentPunishments(string player)
        {
            return Find(LiteDB.Query.And(
                LiteDB.Query.EQ("Receiver", player),
                LiteDB.Query.GT("Expire", DateTime.Now.ToUnixEpochDate())));
        }

        public List<PunishmentDbo> GetPunishments(string player)
        {
            return Find(LiteDB.Query.EQ("Receiver", player));
        }

        public List<PunishmentDbo> GetPunishmentsIssuedBy(string player)
        {
            return Find(LiteDB.Query.EQ("Issuer", player));
        }
    }

    public class PunishmentDbo : IDatabaseEntity
    {
        public int Id { get; set; }
        public PunishmentType Type { get; set; }
        public string Message { get; set; }
        public string Note { get; set; } = "";
        public long Timestamp { get; set; } = DateTime.Now.ToUnixEpochDate();
        public long Expire { get; set; }
        public string Receiver { get; set; }
        public string Issuer { get; set; }

        public int GetId()
        {
            return Id;
        }

        public void Kick(Player player)
        {
            player.Kick(ReasonString());
        }

        public string ReasonString()
        {
            var seconds = Expire - DateTime.Now.ToUnixEpochDate();
            var timeSpan = TimeSpan.FromSeconds(seconds);
            var dString = timeSpan.Days == 0 ? "" : $"{timeSpan.Days}d ";
            var hString = timeSpan.Hours == 0 ? "" : $"{timeSpan.Hours}h ";
            var mString = timeSpan.Minutes == 0 ? "" : $"{timeSpan.Minutes}m ";
            var sString = timeSpan.Seconds == 0 ? "" : $"{timeSpan.Seconds}s";
            return $"You have been banned for {dString}{hString}{hString}{mString}{sString}.\nReason: {Message}";
        }
    }

    public enum PunishmentType
    {
        Ban,
        Kick,
        Warn,
        Mute,
        Other
    }
}