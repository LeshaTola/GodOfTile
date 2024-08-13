using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic.CameraSwitchers;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayInitialState : Modules.StateMachine.States.General.State
    {
        private IInitializeService initializeService;
        private ICameraSwitcher cameraSwitcher;
        
        public GameplayInitialState(string id,IInitializeService initializeService,ICameraSwitcher cameraSwitcher)
            : base(id)
        {
            this.initializeService = initializeService;
            this.cameraSwitcher = cameraSwitcher;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            initializeService.Initialize();
            cameraSwitcher.SwitchCamera("main");
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
            
        }
    }
}