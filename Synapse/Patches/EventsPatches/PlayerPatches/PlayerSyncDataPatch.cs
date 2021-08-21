﻿using System;
using HarmonyLib;
using UnityEngine;
using Logger = Synapse.Api.Logger;

namespace Synapse.Patches.EventsPatches.PlayerPatches
{
    [HarmonyPatch(typeof(AnimationController), nameof(AnimationController.UserCode_CmdSyncData))]
    public class SyncDataPatch
    {
        public static bool Prefix(AnimationController __instance)
        {
            try
            {
                Server.Get.Events.Player.InvokePlayerSyncDataEvent(__instance.GetPlayer(), out bool allow);

                return allow;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Synapse-Event: PlayerSyncData failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
