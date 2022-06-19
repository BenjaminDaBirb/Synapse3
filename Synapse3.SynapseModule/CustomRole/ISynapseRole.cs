﻿using System.Collections.Generic;
using Synapse3.SynapseModule.Enums;
using Synapse3.SynapseModule.Player;

namespace Synapse3.SynapseModule.CustomRole;

public interface ISynapseRole
{
    SynapsePlayer Player { get; set; }

    string GetRoleName();

    int GetRoleID();

    int GetTeamID();

    List<int> GetFriendsID();

    List<int> GetEnemiesID();

    void TryEscape();

    void SpawnPlayer(bool spawnLite);

    void DeSpawn(DespawnReason reason);
}