using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Factories;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.ViewModels;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarner.UI.Providers
{
    public class ResourceEarnerTileSystemUIProvider : IResourceEarnerTileSystemUIProvider
    {
        private ISystemUIFactory systemUIFactory;
        private ILocalizationSystem localizationSystem;

        public ResourceEarnerTileSystemUIProvider(ISystemUIFactory systemUIFactory,
            ILocalizationSystem localizationSystem)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
        }

        public ResourceEarnerSystemUI GetSystem(ResourceEarnerTileSystem tileSystem)
        {
            var systemUI = (ResourceEarnerSystemUI) systemUIFactory.GetSystemUI(tileSystem);
            ResourceEarnerViewModule viewModule = new(localizationSystem, tileSystem);

            systemUI.Init(viewModule);
            return systemUI;
        }
    }
}