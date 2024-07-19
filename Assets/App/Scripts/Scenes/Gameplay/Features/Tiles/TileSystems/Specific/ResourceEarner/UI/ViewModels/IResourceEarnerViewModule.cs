using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.ViewModels
{
    public interface IResourceEarnerViewModule
    {
        ILocalizationSystem LocalizationSystem { get; }
        ResourceEarnerTileSystem TileSystem { get; }
    }
}