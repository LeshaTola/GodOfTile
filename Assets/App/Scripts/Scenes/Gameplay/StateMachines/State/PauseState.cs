using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Screens.Pause;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class PauseState:Modules.StateMachine.States.General.State
    {
        private readonly PausePresenter pausePresenter;
        private readonly ICommandsProvider commandsProvider;
        private readonly IGameInput gameInput;

        public PauseState(string id,
            PausePresenter pausePresenter,
            ICommandsProvider commandsProvider,
            IGameInput gameInput) : base(id)
        {
            this.pausePresenter = pausePresenter;
            this.commandsProvider = commandsProvider;
            this.gameInput = gameInput;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            pausePresenter.Initialize();
            await pausePresenter.Show();
            
            gameInput.OnEscape += OnEscape ;
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            gameInput.OnEscape -= OnEscape;
            
            pausePresenter.Cleanup();
            await pausePresenter.Hide();
        }

        private void OnEscape()
        {
            commandsProvider.GetCommand<GoToGamePlayStateCommand>().Execute();
        }
    }
}