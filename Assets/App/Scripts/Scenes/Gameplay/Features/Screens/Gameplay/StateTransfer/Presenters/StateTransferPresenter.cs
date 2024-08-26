using App.Scripts.Modules.Localization;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransitioner.Presenters
{
    public class StateTransferPresenter : IInitializable, ICleanupable
    {
        private StateTransferView stateTransferView;
        private ICommandsProvider commandsProvider;
        private ILocalizationSystem localizationSystem;
        private readonly IResearchService researchService;

        public StateTransferPresenter(
            StateTransferView stateTransferView,
            ICommandsProvider commandsProvider,
            ILocalizationSystem localizationSystem,
            IResearchService researchService )
        {
            this.stateTransferView = stateTransferView;
            this.commandsProvider = commandsProvider;
            this.localizationSystem = localizationSystem;
            this.researchService = researchService;
        }

        public void Initialize()
        {
            stateTransferView.Initialize(localizationSystem);
            
            stateTransferView.OnBuildingStateButtonClicked += OnBuildingStateButtonClicked;
            stateTransferView.OnBuyAreaStateButtonClicked += OnBuyAreaStateButtonClicked ;
            stateTransferView.OnResearchStateButtonClicked += OnResearchStateButtonClicked;
            
            researchService.OnResearchSystemsCountChanged += OnResearchSystemsCountChanged;
            OnResearchSystemsCountChanged();
        }

        public void Cleanup()
        {
            stateTransferView.Cleanup();
            
            stateTransferView.OnBuildingStateButtonClicked += OnBuildingStateButtonClicked;
            stateTransferView.OnBuyAreaStateButtonClicked += OnBuyAreaStateButtonClicked ;
            stateTransferView.OnResearchStateButtonClicked += OnResearchStateButtonClicked ;
        }

        private void OnBuildingStateButtonClicked()
        {
            commandsProvider?.GetCommand<GoToBuildingStateCommand>().Execute();
        }

        private void OnBuyAreaStateButtonClicked()
        {
            commandsProvider?.GetCommand<GoToBuyAreaStateCommand>().Execute();
        }

        private void OnResearchStateButtonClicked()
        {            
            commandsProvider?.GetCommand<GoToResearchStateCommand>().Execute();
        }

        private void OnResearchSystemsCountChanged()
        {
            if (researchService.ResearchSystems.Count > 0)
            {
                stateTransferView.ShowResearchButton();
                return;
            }
            stateTransferView.HideResearchButton();
        }
    }
}