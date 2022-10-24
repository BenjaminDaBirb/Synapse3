﻿using System.Collections.Generic;
using Synapse3.SynapseModule.Player;

namespace Synapse3.SynapseModule.Database;

public interface IDataBase
{
    public string GetPlayerData(SynapsePlayer player, string key, out bool isHandled);
    public void SetPlayerData(SynapsePlayer player, string key, string value, out bool isHandled);

    public string GetData(string key, out bool isHandled);
    public void SetData(string key, string value, out bool isHandled);

    public Dictionary<string, string> GetLeaderBoard(string key, out bool isHandled, bool orderFromHighest = true, ushort size = 0);
    
    public DataBaseAttribute Attribute { get; set; }

    public void Load();
}