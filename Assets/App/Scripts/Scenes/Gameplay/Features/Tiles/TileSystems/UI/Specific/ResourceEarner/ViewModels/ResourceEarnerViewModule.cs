using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.ViewModels
{
    public class ResourceEarnerViewModule : IResourceEarnerViewModule
    {
        public ResourceEarnerViewModule(ILocalizationSystem localizationSystem)
        {
            LocalizationSystem = localizationSystem;
        }

        public ILocalizationSystem LocalizationSystem { get; }
    }
}