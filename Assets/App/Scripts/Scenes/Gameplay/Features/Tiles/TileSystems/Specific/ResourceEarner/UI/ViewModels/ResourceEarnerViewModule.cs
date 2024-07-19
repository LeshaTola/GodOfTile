using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.ViewModels
{
    public class ResourceEarnerViewModule : IResourceEarnerViewModule
    {
        public ResourceEarnerViewModule(ILocalizationSystem localizationSystem, ResourceEarnerTileSystem tileSystem)
        {
            LocalizationSystem = localizationSystem;
            TileSystem = tileSystem;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ResourceEarnerTileSystem TileSystem { get; }
    }
}