﻿using System;
using HarmonyLib;
using PlayableScps;
using PlayerStatsSystem;
using UnityEngine;

namespace Synapse.Patches.EventsPatches.ScpPatches.Scp096
{
    [HarmonyPatch(typeof(PlayableScps.Scp096), nameof(PlayableScps.Scp096.UpdateVision))]
    internal static class Scp096LookPatch
    {
        [HarmonyPrefix]
        private static bool UpdateVision(PlayableScps.Scp096 __instance)
        {
            try
            {
                var vector = __instance.Hub.transform.TransformPoint(PlayableScps.Scp096._headOffset);
                foreach (var player in Server.Get.Players)
                {
                    if (player.RoleType != RoleType.Spectator && player.Hub != __instance.Hub && !player.ClassManager.IsAnyScp() && Vector3.Dot((player.CameraReference.position - vector).normalized, __instance.Hub.PlayerCameraReference.forward) >= 0.1f)
                    {
                        var visionInformation = VisionInformation.GetVisionInformation(player.Hub, vector, -0.1f, 60f, true, true, __instance.Hub.localCurrentRoomEffects);
                        if (visionInformation.IsLooking)
                        {
                            float delay = visionInformation.LookingAmount / 0.25f * (visionInformation.Distance * 0.1f);
                            if (!__instance.Calming)
                            {
                                if (player.Invisible || Server.Get.Configs.SynapseConfiguration.CantRage096.Contains(player.RoleID))
                                    continue;

                                if (player.RealTeam == Team.SCP && !Server.Get.Configs.SynapseConfiguration.ScpTrigger096)
                                    continue;

                                Server.Get.Events.Scp.Scp096.InvokeScpTargetEvent(player, __instance.GetPlayer(), __instance.PlayerState, out var allow);
                                if (!allow) continue;

                                __instance.AddTarget(player.gameObject);
                            }
                            if (__instance.CanEnrage && player.gameObject != null)
                            {
                                __instance.PreWindup(delay);
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Synapse-Event: Scp096AddTargetEvent failed!!\n{e}");
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(PlayableScps.Scp096), nameof(PlayableScps.Scp096.OnDamage))]
    internal static class Scp096ShootPatch
    {
        [HarmonyPrefix]
        private static bool Damage(PlayableScps.Scp096 __instance, PlayerStatsSystem.DamageHandlerBase handler)
        {
            try
            {
                AttackerDamageHandler attackerDamageHandler = handler as AttackerDamageHandler;
                if (attackerDamageHandler is null || attackerDamageHandler.Attacker.Hub is null || !__instance.CanEnrage) return false;

                var player = attackerDamageHandler.Attacker.Hub.GetPlayer();

                if (player.Invisible || Server.Get.Configs.SynapseConfiguration.CantRage096.Contains(player.RoleID))
                    return false;

                if (player.RealTeam == Team.SCP && !Server.Get.Configs.SynapseConfiguration.ScpTrigger096)
                    return false;

                Server.Get.Events.Scp.Scp096.InvokeScpTargetEvent(player, __instance.GetPlayer(), __instance.PlayerState, out var allow);

                if (allow)
                {
                    __instance.AddTarget(attackerDamageHandler.Attacker.Hub.gameObject);
                    __instance.Windup(false);
                }
                return allow;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Synapse-Event: Scp096AddTargetEvent failed!!\n{e}");
                return true;
            }
        }
    }
}