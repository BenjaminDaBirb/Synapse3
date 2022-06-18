﻿using Neuron.Core.Meta;
using Neuron.Modules.Configs;

namespace Synapse3.SynapseModule.Config;

public class SynapseConfigService : Service
{

    private ConfigService _configService;
    public ConfigContainer Container { get; set; }

    public SynapseConfigService(ConfigService configService)
    {
        _configService = configService;
    }

    public override void Enable()
    {
        Container = _configService.GetContainer("synapse.syml");
    }
}