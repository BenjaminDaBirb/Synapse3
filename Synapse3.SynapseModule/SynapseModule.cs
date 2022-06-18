﻿using Neuron.Core.Modules;
using Neuron.Modules.Patcher;
using Neuron.Modules.Commands;
using Neuron.Modules.Configs;
using Ninject;
using Synapse3.SynapseModule.Player;

namespace Synapse3.SynapseModule;

[Module(
    Name = "Synapse",
    Description = "SCP:SL game functionality",
    Dependencies = new []
    {
        typeof(PatcherModule),
        typeof(CommandsModule),
        typeof(ConfigsModule)
    }
)]
public class SynapseModule : Module
{
    [Inject]
    public PatcherService Patcher { get; set; }
    
    [Inject]
    public CommandService Commands { get; set; }

    public override void Load(IKernel kernel)
    {
        Logger.Info("Synapse3 is loading");
    }

    public override void Enable()
    {
        Logger.Info("Synapse3 enabled!");
    }

    public override void Disable()
    {
        
    }
}