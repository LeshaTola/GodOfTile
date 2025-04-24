using App.Scripts.Features.Tiles.Systems.Views.OnlyText;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.Providers
{
    public class ResearchSystemUIProvider : ISystemUIProvider
    {
        private readonly ILocalizationSystem localizationSystem;
        private readonly ISystemUIFactory systemUIFactory;
        private readonly IResearchService researchService;

        public ResearchSystemUIProvider(ILocalizationSystem localizationSystem, 
            ISystemUIFactory systemUIFactory, 
            IResearchService researchService)
        {
            this.localizationSystem = localizationSystem;
            this.systemUIFactory = systemUIFactory;
            this.researchService = researchService;
        }

        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<OnlyTextSystemSystemUI>();
            var systemData = (ResearchSystemData) tileSystem.Data;
            var text = systemData.Description +
                       $"\nMax: {researchService.ResearchSystems.Count}/{researchService.Config.MaxResearchStation}";
            OnlyTextSystemSystemViewModel viewModule = new(localizationSystem, text);

            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}