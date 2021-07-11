﻿using System;
using HarmonyLib;
using Org.BouncyCastle.Utilities;
using RemoteAdmin;
using Synapse.Api;

namespace Synapse.Client.Patches
{
    [HarmonyPatch(typeof(ServerRoles), nameof(ServerRoles.CallCmdServerSignatureComplete))]
    public static class SignatureCompletePatch
    {
        public static bool Prefix(ServerRoles __instance, string challenge, string response, bool hide)
        {
            if (challenge.Equals("synapse-client-authentication") && SynapseController.ClientManager.IsSynapseClientEnabled)
            {
                try
                {
                    var payload = ClientConnectionData.DecodeJWT(response);
                    __instance._ccm.UserId = payload.Uuid;
                    __instance._ccm.SyncedUserId = payload.Uuid;
                    __instance.PublicKeyAccepted = true;
                    __instance._hub.nicknameSync.UpdateNickname(payload.Sub);
                    ServerConsole.NewPlayers.Add(__instance._ccm);
                    var sessionBytes = Utf8.GetBytes(payload.Session);
                    if (sessionBytes.Length != 24)
                    {
                        Logger.Get.Info("Synapse-Authentication: Wrong Session Token Length?");
                        return true;
                    }
                    var paddedSessionToken = new byte[32];
                    var queryProcessor = __instance.GetComponent<QueryProcessor>();
                    for (int i = 0; i < 24; i++) paddedSessionToken[i] = sessionBytes[i];
                    for (int i = 24; i < 32; i++) paddedSessionToken[i] = 0x00;
                    queryProcessor.Key = paddedSessionToken;
                    queryProcessor.Salt = new byte[32];
                    Arrays.Fill(queryProcessor.Salt, 0x00);
                    queryProcessor.ClientSalt = queryProcessor.Salt;
                    queryProcessor._clientSalt = queryProcessor.ClientSalt;
                    queryProcessor._key = queryProcessor.Key;
                    queryProcessor.CryptoManager.EncryptionKey = queryProcessor.Key;
                    __instance.RefreshPermissions(hide); //Just since its done in base code
                    var playerid = __instance.GetComponent<QueryProcessor>().PlayerId;
                    var uid = __instance._ccm.UserId;
                    
                    if (!ServerRoles.AllowSameAccountJoining)
                    {

                        foreach(var ply in Server.Get.Players)
                        {
                            if(ply.UserId == __instance._ccm.UserId && playerid != ply.PlayerId && !ply.Hub.isDedicatedServer && !ply.Hub.isLocalPlayer)
                            {
                                ServerConsole.AddLog($"Player {__instance._ccm.UserId} ({ply.PlayerId}, {ply.IpAddress}) has been kicked from the server," +
                                    $"because he has just joined the server again from IP address {__instance.connectionToClient.address}");
                                ply.Kick("Only one player instance of the same player is allowed.");
                            }
                        }
                    }

                    var groups = StrippedUser.Resolve(uid, payload.Session).Groups;
                    if(groups != null && groups.Count > 0)
                    {
                        __instance.GetPlayer().GlobalSynapseGroup = groups[0];
                    }
                }
                catch (Exception e)
                {
                    Logger.Get.Error(e);
                }
                return false;
            }

            return true;
        }
    }
}