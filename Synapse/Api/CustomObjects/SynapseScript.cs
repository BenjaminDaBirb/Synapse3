﻿using UnityEngine;

namespace Synapse.Api.CustomObjects
{
    public class SynapseScript : MonoBehaviour
    {
        public PrimitiveSynapseObject SynapseObject { get; set; }

        public void Update()
            => Server.Get.Events.SynapseObject.InvokeUpdate(new Events.SynapseEventArguments.SOEventArgs(SynapseObject));
    }
}