﻿#if DEBUG
using System;

namespace BowieD.Unturned.NPCMaker.Commands
{
    public sealed class CrashCommand : Command
    {
        public override string Name => "crash";
        public override string Help => "Throw unhandled exception in command thread";
        public override string Syntax => "";
        public override void Execute(string[] args)
        {
            MainWindow.Instance.Dispatcher.Invoke(() =>
            {
                throw new Exception("User crashed the app");
            });
        }
    }
}
#endif