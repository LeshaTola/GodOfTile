using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.ViewModels
{
    public class ChangeResourceEarningEffectorViewModule
    {
        public ChangeResourceEarningEffectorViewModule(ILocalizationSystem localizationSystem,
            ChangeResourceEarningEffect effect)
        {
            LocalizationSystem = localizationSystem;
            Effect = effect;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ChangeResourceEarningEffect Effect { get; }
    }
}