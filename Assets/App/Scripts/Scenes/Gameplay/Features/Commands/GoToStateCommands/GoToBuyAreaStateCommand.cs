using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.StateMachines.State;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.GoToGamePlayStateCommands
{
    public class GoToBuyAreaStateCommand : LabeledCommand
    {
        private StateMachine stateMachine;

        public GoToBuyAreaStateCommand(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override async void Execute()
        {
            await stateMachine.ChangeState(StatesIds.BUY_AREA_STATE);
        }
    }
}