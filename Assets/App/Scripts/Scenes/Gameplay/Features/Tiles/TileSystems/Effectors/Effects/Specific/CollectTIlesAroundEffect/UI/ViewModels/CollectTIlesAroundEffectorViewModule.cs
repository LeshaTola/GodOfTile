using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.
    UI.ViewModels
{
    public class CollectTilesAroundEffectorViewModule
    {
        public CollectTilesAroundEffectorViewModule(ILocalizationSystem localizationSystem,
            CollectTilesAroundEffect effect)
        {
            LocalizationSystem = localizationSystem;
            Effect = effect;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public CollectTilesAroundEffect Effect { get; }
    }
}