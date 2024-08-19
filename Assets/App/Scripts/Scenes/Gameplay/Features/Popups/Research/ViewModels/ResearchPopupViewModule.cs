using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.ViewModels
{
    public class ResearchPopupViewModule
    {
        public ResearchPopupViewModule(ILocalizationSystem localizationSystem, IResearchService researchService)
        {
            LocalizationSystem = localizationSystem;
            ResearchService = researchService;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public IResearchService ResearchService { get; }
    }
}