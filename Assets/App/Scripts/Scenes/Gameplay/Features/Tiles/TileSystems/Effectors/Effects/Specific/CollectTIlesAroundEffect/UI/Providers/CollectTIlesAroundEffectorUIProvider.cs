using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.CollectTIlesAroundEffect.UI.Providers
{
    public class CollectTilesAroundEffectorUIProvider : ISystemUIProvider
    {
        private ISystemUIFactory systemUIFactory;
        private ILocalizationSystem localizationSystem;

        public CollectTilesAroundEffectorUIProvider(ISystemUIFactory systemUIFactory,
            ILocalizationSystem localizationSystem)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
        }

        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<ChangeResourceEarningEffectorUI>();
            var effectorData = (EffectorData) tileSystem.Data;
            var effect = (ChangeResourceEarningEffect.ChangeResourceEarningEffect) effectorData.Effect;
            var viewModule = new ChangeResourceEarningEffectorViewModule(localizationSystem, effect);

            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}