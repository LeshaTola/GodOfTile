using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayState : Modules.StateMachine.States.General.State
    {
        private IUpdateService updateService;
        private IGameInput gameInput;
        private ICommandsProvider commandsProvider;
        private ITileSelectionProvider tileSelectionProvider;
        private readonly GameplayScreenPresenter gameplayScreenPresenter;
        private readonly ICameraController cameraController;

        public GameplayState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ICommandsProvider commandsProvider,
            ITileSelectionProvider tileSelectionProvider,
            GameplayScreenPresenter gameplayScreenPresenter,
            ICameraController cameraController
        )
            : base(id)
        {
            this.updateService = updateService;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
            this.tileSelectionProvider = tileSelectionProvider;
            this.gameplayScreenPresenter = gameplayScreenPresenter;
            this.cameraController = cameraController;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            cameraController.Active = true;

            gameplayScreenPresenter.Initialize();
            await gameplayScreenPresenter.Show();

            gameInput.OnBuild += OnBuild;
            gameInput.OnI += OnI;
            gameInput.OnM += OnM;
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

                tileSelectionProvider.SelectTile(tile);
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            gameInput.OnBuild -= OnBuild;
            gameInput.OnI -= OnI;
            gameInput.OnM -= OnM;

            gameplayScreenPresenter.Cleanup();
            tileSelectionProvider.Cleanup();

            await gameplayScreenPresenter.Hide();
            cameraController.Active = false;
        }

        private void OnBuild()
        {
            commandsProvider.GetCommand<GoToBuildingStateCommand>().Execute();
        }

        private void OnM()
        {
            commandsProvider.GetCommand<GoToBuyAreaStateCommand>().Execute();
        }

        private void OnI()
        {
            commandsProvider.GetCommand<GoToResearchStateCommand>().Execute();
        }
    }
}