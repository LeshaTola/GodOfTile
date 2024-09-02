using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class BuildState : Modules.StateMachine.States.General.State
    {
        private IGameInput gameInput;
        private IVisualizer gridVisualizer;
        private IUpdateService updateService;
        private ShopScreenPresenter shopScreenPresenter;
        private ICameraController cameraController;
        private ITilesCreationService creationService;

        public BuildState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ITilesCreationService creationService,
            IVisualizer gridVisualizer,
            ShopScreenPresenter shopScreenPresenter,
            ICameraController cameraController
        )
            : base(id)
        {
            this.updateService = updateService;
            this.gameInput = gameInput;
            this.creationService = creationService;
            this.gridVisualizer = gridVisualizer;
            this.shopScreenPresenter = shopScreenPresenter;
            this.cameraController = cameraController;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            cameraController.Active = true;

            shopScreenPresenter.Initialize();
            shopScreenPresenter.Setup();
            await shopScreenPresenter.Show();

            gameInput.OnEscape += OnBuild;
            gameInput.OnBuild += OnBuild;
            gameInput.OnRotate += OnRotate;

            creationService.StartPlacingTile();
            gridVisualizer.Show();
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

            gameInput.OnEscape -= OnBuild;
            gameInput.OnBuild -= OnBuild;
            gameInput.OnRotate -= OnRotate;

            cameraController.Active = false;
            creationService.StopPlacingTile();
            gridVisualizer.Hide();

            shopScreenPresenter.Cleanup();
            await shopScreenPresenter.Hide();
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