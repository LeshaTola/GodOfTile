using Assets.App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Assets.App.Scripts.Scenes.Gameplay.StateMachines.States;
using Modules.StateMachineCore;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Commands.GoToGamePlayStateCommands
{
    public class GoToGamePlayStateCommand : LabeledCommand
    {
        private StateMachine stateMachine;

        public GoToGamePlayStateCommand(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override void Execute()
        {
            stateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}
