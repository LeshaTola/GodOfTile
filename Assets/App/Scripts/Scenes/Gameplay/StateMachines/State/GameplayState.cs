using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
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
        private ICommandsProvider commandsProvider;
        private ITileSelectionProvider tileSelectionProvider;
        private IGameplayPopupRouter gameplayPopupRouter;

        public GameplayState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ICommandsProvider commandsProvider,
            ITileSelectionProvider tileSelectionProvider,
            IGameplayPopupRouter gameplayPopupRouter
        )
            : base(id)
        {
            this.updateService = updateService;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
            this.tileSelectionProvider = tileSelectionProvider;
            this.gameplayPopupRouter = gameplayPopupRouter;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await gameplayPopupRouter.Show();
            
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

                await StateMachine.ChangeState(StatesIds.INFORMATION_STATE);
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            
            gameInput.OnBuild -= OnBuild;
            gameInput.OnI -= OnI;
            gameInput.OnM -= OnM;
            
            await gameplayPopupRouter.Hide();
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