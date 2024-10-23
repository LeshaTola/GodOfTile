using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands
{
    public class GoToPauseState:LabeledCommand
    {
        private StateMachine stateMachine;

        public GoToPauseState(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override async void Execute()
        {
            await stateMachine.ChangeState(StatesIds.PAUSE_STATE);
        }
    }
}
