using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Visualizators;
using Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers;
using Cysharp.Threading.Tasks;
using Features.StateMachineCore;
using Features.StateMachineCore.States;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class BuildState : State
    {
        private List<IUpdatable> updatables;
        private IGameInput gameInput;
        private ITilesCreationService creationService;
        private IGridVisualizator gridVisualizator;
        private IShopPopupRouter shopPopupRouter;

        public BuildState(
            string id,
            List<IUpdatable> updatables,
            IGameInput gameInput,
            ITilesCreationService creationService,
            IGridVisualizator gridVisualizator,
            IShopPopupRouter shopPopupRouter
        )
            : base(id)
        {
            this.updatables = updatables;
            this.gameInput = gameInput;
            this.creationService = creationService;
            this.gridVisualizator = gridVisualizator;
            this.shopPopupRouter = shopPopupRouter;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            gameInput.OnBuild += OnBuild;
            gameInput.OnRotate += OnRotate;

            creationService.StartPlacingTile();
            gridVisualizator.ShowGrid();
            await shopPopupRouter.ShowShopPopup();
        }

        public override async UniTask Update()
        {
            await base.Update();

            foreach (var updatable in updatables)
            {
                updatable.Update();
            }

            creationService.MoveActiveTile(gameInput.GetGridMousePosition());

            if (gameInput.IsMouseClicked())
            {
                creationService.PlaceActiveTile();
                creationService.StartPlacingTile();
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            creationService.StopPlacingTile();
            gridVisualizator.HideGrid();
            await shopPopupRouter.HideShopPopup();

            gameInput.OnBuild -= OnBuild;
            gameInput.OnRotate -= OnRotate;
        }

        private async void OnBuild()
        {
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }

        private void OnRotate()
        {
            creationService.RotateActiveTile();
        }
    }
}
