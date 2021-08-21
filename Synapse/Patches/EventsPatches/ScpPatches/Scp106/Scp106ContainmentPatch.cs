﻿using System;
using HarmonyLib;
using Synapse.Api;
using EventHandler = Synapse.Api.Events.EventHandler;

namespace Synapse.Patches.EventsPatches.ScpPatches.Scp106
{
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.UserCode_CmdContain106))]
    internal class Scp106ContainmentPatch
    {
        private static bool Prefix(PlayerInteract __instance)
        {
            try
            {
                var player = __instance.GetPlayer();
                if (!SynapseExtensions.CanHarmScp(player)) return false;

                var allow = true;
                EventHandler.Get.Scp.Scp106.InvokeScp106ContainmentEvent(player, ref allow);
                return allow;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Synapse-Event: Scp106Containment failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}