using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.StateMachines.State;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.GoToGamePlayStateCommands
{
    public class GoToGamePlayStateCommand : LabeledCommand
    {
        private StateMachine stateMachine;

        public GoToGamePlayStateCommand(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override async void Execute()
        {
            await stateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}