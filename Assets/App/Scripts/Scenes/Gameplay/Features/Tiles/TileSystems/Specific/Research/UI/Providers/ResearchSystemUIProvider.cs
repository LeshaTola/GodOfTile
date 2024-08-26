using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.OnlyTextSystem.UI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.OnlyTextSystem.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.Providers
{
    public class ResearchSystemUIProvider: ISystemUIProvider
    {
        public ResearchSystemUIProvider(ILocalizationSystem localizationSystem, ISystemUIFactory systemUIFactory)
        {
            this.localizationSystem = localizationSystem;
            this.systemUIFactory = systemUIFactory;
        }

        private ILocalizationSystem localizationSystem;
        private ISystemUIFactory systemUIFactory;
        
        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<OnlyTextSystemSystemUI>();
            var systemData = (ResearchSystemData) tileSystem.Data;
            OnlyTextSystemSystemViewModel viewModule = new(localizationSystem, systemData.Description);

            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}