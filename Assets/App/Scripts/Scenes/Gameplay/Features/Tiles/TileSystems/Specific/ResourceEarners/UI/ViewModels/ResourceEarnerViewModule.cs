using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.ViewModels
{
    public class ResourceEarnerViewModule : IResourceEarnerViewModule
    {
        public ResourceEarnerViewModule(ILocalizationSystem localizationSystem, ResourceEarnerSystemData systemData)
        {
            LocalizationSystem = localizationSystem;
            SystemData = systemData;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ResourceEarnerSystemData SystemData { get; }
    }
}