using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.ViewModels;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers
{
    public class ResourceEarnerUIProvider : IResourceEarnerUIProvider
    {
        private ISystemUIFactory systemUIFactory;
        private ILocalizationSystem localizationSystem;

        public ResourceEarnerUIProvider(ISystemUIFactory systemUIFactory,
            ILocalizationSystem localizationSystem)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
        }

        public ResourceEarnerUI GetSystem(ResourceEarner tileSystem)
        {
            var systemUI = (ResourceEarnerUI) systemUIFactory.GetSystemUI(tileSystem);
            var systemData = (ResourceEarnerSystemData) tileSystem.Data;
            ResourceEarnerViewModule viewModule = new(localizationSystem, systemData);

            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}