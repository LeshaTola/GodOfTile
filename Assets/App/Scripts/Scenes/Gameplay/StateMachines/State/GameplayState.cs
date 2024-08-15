using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.Routers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayState : Modules.StateMachine.States.General.State
    {
        private IUpdateService updateService;
        private IGameInput gameInput;
        private ITileSelectionProvider tileSelectionProvider;
        private IGameplayPopupRouter gameplayPopupRouter; 
        public GameplayState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ITileSelectionProvider tileSelectionProvider,
            IGameplayPopupRouter gameplayPopupRouter
        )
            : base(id)
        {
            this.updateService = updateService;
            this.gameInput = gameInput;
            this.tileSelectionProvider = tileSelectionProvider;
            this.gameplayPopupRouter = gameplayPopupRouter;
        }

        public override async UniTask Enter()
        {
            await base.Enter(); 
            await gameplayPopupRouter.Show();
            gameInput.OnBuild += OnBuild;
        }

        public override async UniTask Update()
        {
            await base.Update();
            updateService.Update();

            if (gameInput.IsMouseClicked())
            {
                var tile = tileSelectionProvider.GetTileAtMousePosition();

                if (tile == null)
                {
                    return;
                }

                await StateMachine.ChangeState(StatesIds.INFORMATION_STATE);
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            gameInput.OnBuild -= OnBuild;

            await gameplayPopupRouter.Hide();
        }

        private async void OnBuild()
        {
            await StateMachine.ChangeState(StatesIds.BUILDING_STATE);
        }
    }
}