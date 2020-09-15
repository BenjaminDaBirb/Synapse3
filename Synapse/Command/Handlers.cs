﻿namespace Synapse.Command
{
    public class Handlers
    {
        internal Handlers() { }

        public CommandHandler RemoteAdminHandler { get; } = new CommandHandler();

        public CommandHandler ClientCommandHandler { get; } = new CommandHandler();

        public CommandHandler ServerConsoleHandler { get; } = new CommandHandler();
        
    }
}