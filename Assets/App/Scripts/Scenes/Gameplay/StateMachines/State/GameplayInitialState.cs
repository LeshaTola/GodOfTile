using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayInitialState : Modules.StateMachine.States.General.State
    {
        public GameplayInitialState(string id)
            : base(id)
        {
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}