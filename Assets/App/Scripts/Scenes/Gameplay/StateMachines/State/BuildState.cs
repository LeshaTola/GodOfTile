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

        public override void Enter()
        {
            base.Enter();

            gameInput.OnBuild += OnBuild;
            gameInput.OnRotate += OnRotate;

            creationService.StartPlacingTile();
            gridVisualizator.ShowGrid();
            shopPopupRouter.ShowShopPopup().Forget();
        }

        public override void Update()
        {
            base.Update();

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

        public override void Exit()
        {
            base.Exit();

            creationService.StopPlacingTile();
            gridVisualizator.HideGrid();
            shopPopupRouter.HideShopPopup().Forget();

            gameInput.OnBuild -= OnBuild;
            gameInput.OnRotate -= OnRotate;
        }

        private void OnBuild()
        {
            StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }

        private void OnRotate()
        {
            creationService.RotateActiveTile();
        }
    }
}
