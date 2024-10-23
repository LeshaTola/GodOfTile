using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Sounds;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransfer.Presenters
{
    public class StateTransferPresenter : IInitializable, ICleanupable
    {
        private StateTransferView view;
        private ICommandsProvider commandsProvider;
        private ILocalizationSystem localizationSystem;
        private readonly IResearchService researchService;
        private readonly ISoundProvider soundProvider;

        public StateTransferPresenter(
            StateTransferView view,
            ICommandsProvider commandsProvider,
            ILocalizationSystem localizationSystem,
            IResearchService researchService,
            ISoundProvider soundProvider)
        {
            this.view = view;
            this.commandsProvider = commandsProvider;
            this.localizationSystem = localizationSystem;
            this.researchService = researchService;
            this.soundProvider = soundProvider;
        }

        public void Initialize()
        {
            view.Initialize(localizationSystem);

            view.OnBuildingStateButtonClicked += OnBuildingStateButtonClicked;
            view.OnBuyAreaStateButtonClicked += OnBuyAreaStateButtonClicked;
            view.OnResearchStateButtonClicked += OnResearchStateButtonClicked;

            researchService.OnResearchSystemsCountChanged += OnResearchSystemsCountChanged;
            OnResearchSystemsCountChanged();
        }

        public void Cleanup()
        {
            view.Cleanup();

            view.OnBuildingStateButtonClicked -= OnBuildingStateButtonClicked;
            view.OnBuyAreaStateButtonClicked -= OnBuyAreaStateButtonClicked;
            view.OnResearchStateButtonClicked -= OnResearchStateButtonClicked;

            researchService.OnResearchSystemsCountChanged -= OnResearchSystemsCountChanged;
        }

        private void OnBuildingStateButtonClicked()
        {
            commandsProvider?.GetCommand<GoToBuildingStateCommand>().Execute();
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void OnBuyAreaStateButtonClicked()
        {
            commandsProvider?.GetCommand<GoToBuyAreaStateCommand>().Execute();
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void OnResearchStateButtonClicked()
        {
            commandsProvider?.GetCommand<GoToResearchStateCommand>().Execute();
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void OnResearchSystemsCountChanged()
        {
            if (researchService.ResearchSystems.Count > 0)
            {
                view.ShowResearchButton();
                return;
            }

            view.HideResearchButton();
        }

        public async UniTask Show()
        {
            await view.Show();
        }

        public async UniTask Hide()
        {
            await view.Hide();
        }
    }
}