using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.ViewModels
{
    public interface IResourceEarnerViewModule
    {
        ILocalizationSystem LocalizationSystem { get; }
        ResourceEarnerSystemData SystemData { get; }
    }
}