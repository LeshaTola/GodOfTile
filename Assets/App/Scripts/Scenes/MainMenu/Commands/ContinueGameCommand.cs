using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Scenes.MainMenu.Commands
{
    public class ContinueGameCommand : LabeledCommand
    {
        private StateMachine stateMachine;

        public ContinueGameCommand(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override async void Execute()
        {
            //await stateMachine.ChangeState();
        }
    }
}