using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.ViewModels
{
    public class SpeedupResourceEarningEffectorViewModule
    {
        public SpeedupResourceEarningEffectorViewModule(ILocalizationSystem localizationSystem,
            SpeedupResourceEarningEffectorData data)
        {
            LocalizationSystem = localizationSystem;
            Data = data;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public SpeedupResourceEarningEffectorData Data { get; }
    }
}