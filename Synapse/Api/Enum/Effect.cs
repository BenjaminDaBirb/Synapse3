﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Synapse.Api.Enum
{
    public enum Effect
    {
        /// <summary>
        /// The Player can't open their inventory and reload their weapons
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Amnesia,
        [Obsolete("ArtificialRegen is no longer in the game", true)]
        ArtificialRegen,
        /// <summary>
        /// Quickly drains stamina then health if there is none left
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Asphyxiated,
        /// <summary>
        /// Decreasing damage over time. Ticks every 5s.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Bleeding,
        /// <summary>
        /// Applies extreme screen blur
        /// </summary>
        Blinded,
        /// <summary>
        /// Slightly increases all damage taken
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Burned,
        /// <summary>
        /// Blurs the screen as the Player turns
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Concussed,
        /// <summary>
        /// Teleports to the pocket dimension and drains hp until he escapes
        /// </summary>
        /// <remarks>1 = Enabled</remarks>
        Corroding,
        /// <summary>
        /// Heavily muffles all sounds
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Deafened,
        /// <summary>
        /// Remove 10% of max health each second
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Decontaminating,
        /// <summary>
        /// Slows all movement
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Disabled,
        [Obsolete("Disarmed is no longer in the game", true)]
        Disarmed,
        [Obsolete("Discharge is no longer in the game", true)]
        Discharge,
        /// <summary>
        /// Prevents all movement
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Ensnared,
        /// <summary>
        /// Laves stamina capacity and regeneration rate
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Exhausted,
        [Obsolete("Exsanguination is no longer in the game", true)]
        Exsanguination,
        /// <summary>
        /// Flash the Player
        /// </summary>
        /// <remarks>0 = Disabled, 1-244 = time in ms 255 = forever</remarks>
        Flashed,
        /// <summary>
        /// Sprinting drains 2 hp/s
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Hemorrhage,
        /// <summary>
        /// Infinite stamina
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Invigorated,
        [Obsolete("Panic is no longer in the game", true)]
        Panic,
        /// <summary>
        /// Ascending damage over time. Ticks every 5s.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Poisoned,
        /// <summary>
        /// The Player will walk faster
        /// </summary>
        /// <remarks>0 = Disabled, 1 = 1xCola, 2 = 2xCola, 3 = 3xCola, 4 = 4xCola</remarks>
        Scp207,
        /// <summary>
        /// use <see cref="Invisible"/>
        /// </summary>
        [Obsolete("Scp268 effect is no longer in the game, use Invisible", true)]
        Scp268,
        /// <summary>
        /// Slows down player (No effect on SCPs)
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        SinkHole,
        /// <summary>
        /// The vision of SCP-939
        /// </summary>
        /// <remarks>0 = Disabled, 1 = OnlyMarker, 2 = Only Screen, 3 = Everything</remarks>
        Visuals939,
        /// <summary>
        /// Reduces damage taken from shots
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        BodyshotReduction,
        /// <summary>
        /// Reduces all forms of damage
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        DamageReduction,
        /// <summary>
        /// Reduces player vision and weapon accuracy. Prevents Hume Shield from regenerating. Humans take damage overtime.
        /// </summary>
        Hypothermia,
        /// <summary>
        /// The Player can't be seen by other entities.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Invisible,
        /// <summary>
        /// Increases movement speed
        /// </summary>
        /// <remarks>0 = Disabled, each intensity point adds 1% of movement speed (max 255)</remarks>
        MovementBoost,
        /// <summary>
        /// Reduces severity of Amnesia, Bleeding, Burned, Concussed, Hemorrhage, Poisoned and SCP-207.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        RainbowTaste,
        /// <summary>
        /// Removes the player's hands and ability to open inventory or interact; Slowly drains HP.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        SeveredHands,
        /// <summary>
        /// Reduces player speed by 20%; SCPs are immune to this effect.	
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Stained,
        /// <summary>
        /// Immunity to negative status effects except decontamination and pocket dimension.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Vitality,
        /// <summary>
        /// Turns the screen black.
        /// </summary>
        /// <remarks>0 = Disabled, 1 = Enabled</remarks>
        Visuals173Blink
    }
}
