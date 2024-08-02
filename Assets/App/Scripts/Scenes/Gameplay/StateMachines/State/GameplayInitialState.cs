using App.Scripts.Modules.StateMachine.Services.InitializeService;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayInitialState : Modules.StateMachine.States.General.State
    {
        private IInitializeService initializeService;
        public GameplayInitialState(string id,IInitializeService initializeService)
            : base(id)
        {
            this.initializeService = initializeService;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            initializeService.Initialize();
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}