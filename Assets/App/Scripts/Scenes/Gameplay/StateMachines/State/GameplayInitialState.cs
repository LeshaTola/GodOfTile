using Cysharp.Threading.Tasks;
using Features.StateMachineCore.States;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class GameplayInitialState : State
    {
        public GameplayInitialState(string id)
            : base(id) { }

        public override async UniTask Enter()
        {
            await base.Enter();
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}
