﻿using System;
using Harmony;
using Mirror;
using Synapse.Api;
using Synapse.Api.Enum;
using UnityEngine;
using Logger = Synapse.Api.Logger;

namespace Synapse.Events.Patches
{
	
	[HarmonyPatch(typeof(Generator079), nameof(Generator079.Interact))]
	internal static class GeneratorPatche
	{
		private static bool Prefix(Generator079 __instance, GameObject person, PlayerInteract.Generator079Operations command)
		{
			try
			{
				var player = person.GetPlayer();
				var generator = __instance.GetGenerator();

				switch (command)
				{
					case PlayerInteract.Generator079Operations.Tablet:

						if (generator.IsTabledConnected || !generator.Open || __instance._localTime <= 0f || Generator079.mainGenerator.forcedOvercharge)
							return false;

						Inventory component = person.GetComponent<Inventory>();

						using (SyncList<Inventory.SyncItemInfo>.SyncListEnumerator enumerator = component.items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Inventory.SyncItemInfo syncItemInfo = enumerator.Current;
								if (syncItemInfo.id == ItemType.WeaponManagerTablet)
								{
									bool allow2 = true;
									Server.Get.Events.Player.InvokePlayerGeneratorInteractEvent(player, generator,GeneratorInteraction.TabletInjected, ref allow2);
									if (!allow2) break;

									component.items.Remove(syncItemInfo);
									generator.IsTabledConnected = true;
									break;
								}
							}
						}
						return false;

					case PlayerInteract.Generator079Operations.Cancel:
						if (!generator.IsTabledConnected) return false;

						bool allow = true;
						Server.Get.Events.Player.InvokePlayerGeneratorInteractEvent(player, generator, GeneratorInteraction.TabledEjected, ref allow);
						return allow;
				}
				return true;
			}
			catch (Exception e)
			{
				Logger.Get.Error($"GeneratorTablet Event Error: {e}");
				return true;
			}
		}
	}

	[HarmonyPatch(typeof(Generator079), nameof(Generator079.OpenClose))]
	public static class GeneratorDoorPatches
	{
		public static bool Prefix(Generator079 __instance, GameObject person)
		{
			var player = person.GetPlayer();
			var generator = __instance.GetGenerator();

			if (player.Inventory == null || __instance._doorAnimationCooldown > 0f || __instance._deniedCooldown > 0f) return false;

			if (generator.Locked)
			{
				var allow = true;
				if (!generator.Open)
				{
					Server.Get.Events.Player.InvokePlayerGeneratorInteractEvent(player, generator, GeneratorInteraction.OpenDoor, ref allow);
				}
				else
				{
					Server.Get.Events.Player.InvokePlayerGeneratorInteractEvent(player, generator, GeneratorInteraction.CloseDoor, ref allow);
				}

				if (!allow)
				{
					__instance.RpcDenied();
					return false;
				}

				generator.Open = !generator.Open;
				return false;
			}

			//Unlock The Generator
			var flag = player.Bypass;
			var flag2 = player.Team != Team.SCP;

			if (flag2 && player.Inventory.curItem > ItemType.KeycardJanitor)
			{
				var permissions = player.Inventory.GetItemByID(player.Inventory.curItem).permissions;

				foreach (var t in permissions)
					if (t == "ARMORY_LVL_2")
						flag = true;
			}

			Server.Get.Events.Player.InvokePlayerGeneratorInteractEvent(player, generator, GeneratorInteraction.Unlocked, ref flag);

			if (flag)
			{
				generator.Locked = false;
				return false;
			}
			__instance.RpcDenied();

			return false;
		}
	}
	
}
