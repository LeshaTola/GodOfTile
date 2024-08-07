﻿using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Grid.Visualizators;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class BuildState : Modules.StateMachine.States.General.State
    {
        private IUpdateService updateService;
        private IGameInput gameInput;
        private ITilesCreationService creationService;
        private IGridVisualizator gridVisualizator;
        private IShopPopupRouter shopPopupRouter;

        public BuildState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ITilesCreationService creationService,
            IGridVisualizator gridVisualizator,
            IShopPopupRouter shopPopupRouter
        )
            : base(id)
        {
            this.updateService = updateService;
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

            updateService.Update();

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