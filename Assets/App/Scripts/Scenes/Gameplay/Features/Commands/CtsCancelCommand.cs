using System.Threading;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Scenes.Gameplay.Features.Commands
{
    public class CtsCancelCommand : LabeledCommand
    {
        private readonly CancellationTokenSource cts;

        public CtsCancelCommand(string label, CancellationTokenSource cts)
            : base(label)
        {
            this.cts = cts;
        }

        public override void Execute()
        {
            cts.Cancel();
        }
    }
}