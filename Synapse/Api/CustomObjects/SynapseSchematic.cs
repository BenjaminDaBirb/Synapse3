﻿using Synapse.Api.Enum;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Synapse.Api.CustomObjects
{
    public class SynapseSchematic : IConfigSection
    {
        [NonSerialized]
        internal bool reload = true;

        public int ID { get; internal set; }
        public string Name { get; internal set; }

        public List<PrimitiveConfiguration> PrimitiveObjects { get; set; } = new List<PrimitiveConfiguration>();
        public List<LightSourceConfiguration> LightObjects { get; set; } = new List<LightSourceConfiguration>();
        public List<TargetConfiguration> TargetObjects { get; set; } = new List<TargetConfiguration>();
        public List<ItemConfiguration> ItemObjects { get; set; } = new List<ItemConfiguration>();
        public List<WorkStationConfiguration> WorkStationObjects { get; set; } = new List<WorkStationConfiguration>();

        public class PrimitiveConfiguration
        {
            public PrimitiveType PrimitiveType { get; set; }

            public SerializedVector3 Position { get; set; }

            public SerializedVector3 Rotation { get; set; }

            public SerializedVector3 Scale { get; set; }

            public SerializedColor Color { get; set; }
        }

        public class LightSourceConfiguration
        {
            public SerializedVector3 Position { get; set; }

            public SerializedVector3 Rotation { get; set; }

            public SerializedVector3 Scale { get; set; }

            public SerializedColor Color { get; set; }

            public float LightIntensity { get; set; }

            public float LightRange { get; set; }

            public bool LightShadows { get; set; }
        }

        public class TargetConfiguration
        {
            public TargetType TargetType { get; set; }

            public SerializedVector3 Position { get; set; }

            public SerializedVector3 Rotation { get; set; }

            public SerializedVector3 Scale { get; set; }
        }

        public class ItemConfiguration
        {
            public ItemType ItemType { get; set; }

            public SerializedVector3 Position { get; set; }

            public SerializedVector3 Rotation { get; set; }

            public SerializedVector3 Scale { get; set; }
        }

        public class WorkStationConfiguration
        {
            public SerializedVector3 Position { get; set; }

            public SerializedVector3 Rotation { get; set; }

            public SerializedVector3 Scale { get; set; }
        }
    }
}
