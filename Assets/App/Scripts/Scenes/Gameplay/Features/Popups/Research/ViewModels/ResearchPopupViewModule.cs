using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Level;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.ViewModels
{
    public class ResearchPopupViewModule
    {
        public ResearchPopupViewModule(ILocalizationSystem localizationSystem, IResearchService researchService,
            IFactory<LevelElement> levelsFactory, IFactory<ResearchElement> researchesFactory, IInformationWidgetViewModule informationWidgetViewModule, IInventorySystem inventorySystem)
        {
            LocalizationSystem = localizationSystem;
            ResearchService = researchService;
            LevelsFactory = levelsFactory;
            ResearchesFactory = researchesFactory;
            InformationWidgetViewModule = informationWidgetViewModule;
            InventorySystem = inventorySystem;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public IResearchService ResearchService { get; }
        public IInventorySystem InventorySystem { get; }
        public IFactory<ResearchElement> ResearchesFactory { get; }
        public IFactory<LevelElement> LevelsFactory { get; }
        public IInformationWidgetViewModule InformationWidgetViewModule { get; }
    }
}