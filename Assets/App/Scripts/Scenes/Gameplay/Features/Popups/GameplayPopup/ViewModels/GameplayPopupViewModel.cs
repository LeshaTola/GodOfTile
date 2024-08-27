using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.ViewModels
{
    public class GameplayPopupViewModel : IGameplayPopupViewModel
    {
        public GameplayPopupViewModel(ILocalizationSystem localizationSystem, ILabeledCommand buildStateCommand,
            ILabeledCommand buyAreaStateCommand, IResearchService researchService)
        {
            LocalizationSystem = localizationSystem;
            BuildStateCommand = buildStateCommand;
            BuyAreaStateCommand = buyAreaStateCommand;
            ResearchService = researchService;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ILabeledCommand BuildStateCommand { get; }
        public ILabeledCommand BuyAreaStateCommand { get; }
        public IResearchService ResearchService { get; }
    }
}