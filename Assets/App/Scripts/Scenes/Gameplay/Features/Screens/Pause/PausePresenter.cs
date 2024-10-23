using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Pause
{
    public class PausePresenter: IInitializable, ICleanupable
    {
        private PauseView view;
        private ILocalizationSystem localizationSystem;
        private ICommandsProvider commandsProvider;
        private readonly ISoundProvider soundProvider;

        public PausePresenter(PauseView view, ILocalizationSystem localizationSystem, ICommandsProvider commandsProvider, ISoundProvider soundProvider)
        {
            this.view = view;
            this.localizationSystem = localizationSystem;
            this.commandsProvider = commandsProvider;
            this.soundProvider = soundProvider;
        }

        public void Initialize()
        {
            view.Initialize(localizationSystem);

            view.OnContinueButtonClicked += OnContinueButtonClicked;
            view.OnSettingsButtonClicked += OnSettingsButtonClicked;
            view.OnExitButtonClicked += OnExitButtonClicked;
            
        }

        public void Cleanup()
        {
            view.Cleanup();

            view.OnContinueButtonClicked -= OnContinueButtonClicked;
            view.OnSettingsButtonClicked -= OnSettingsButtonClicked;
            view.OnExitButtonClicked -= OnExitButtonClicked;
        }
        
        public async UniTask Show()
        {
            await view.Show();

        }

        public async UniTask Hide()
        {
            await view.Hide();
        }

        private void OnContinueButtonClicked()
        {
            commandsProvider?.GetCommand<GoToGamePlayStateCommand>().Execute();
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void OnSettingsButtonClicked()
        {
            commandsProvider?.GetCommand<GoToLoadSceneState>().Execute();
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void OnExitButtonClicked()
        {
            commandsProvider?.GetCommand<GoToLoadSceneState>().Execute();
            soundProvider.PlaySound(view.ButtonSoundKey);
        }
    }
}