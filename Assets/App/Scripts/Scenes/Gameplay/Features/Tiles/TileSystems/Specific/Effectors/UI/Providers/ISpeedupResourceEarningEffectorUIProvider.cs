using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.Providers
{
    public class SpeedupResourceEarningEffectorUIProvider: ISystemUIProvider
    {
        private ISystemUIFactory systemUIFactory;
        private ILocalizationSystem localizationSystem;

        public SpeedupResourceEarningEffectorUIProvider(ISystemUIFactory systemUIFactory, ILocalizationSystem localizationSystem)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
        }

        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = (SpeedupResourceEarningEffectorUI) systemUIFactory.GetSystemUI(tileSystem);
            var systemData = (SpeedupResourceEarningEffectorData) tileSystem.Data;
            var viewModule = new SpeedupResourceEarningEffectorViewModule(localizationSystem, systemData);

            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
    
    
}