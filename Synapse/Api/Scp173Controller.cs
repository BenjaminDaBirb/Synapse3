﻿using System.Collections.Generic;

namespace Synapse.Api
{
    public class Scp173Controller
    {
        internal Scp173Controller() { }

        public HashSet<Player> IgnoredPlayers { get; internal set; } = new HashSet<Player>();

        public HashSet<Player> TurnedPlayers { get; internal set; } = new HashSet<Player>();

        public HashSet<Player> ConfrontingPlayers { get; internal set; } = new HashSet<Player>();
    }
}
