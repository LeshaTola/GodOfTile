using App.Scripts.Features.Tiles.Systems.Views.OnlyText;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Defence
{
    public class DefenceEffectUIProvider : ISystemUIProvider
    {
        private readonly ILocalizationSystem localizationSystem;
        private readonly ISystemUIFactory systemUIFactory;

        public DefenceEffectUIProvider(ILocalizationSystem localizationSystem,
            ISystemUIFactory systemUIFactory)
        {
            this.localizationSystem = localizationSystem;
            this.systemUIFactory = systemUIFactory;
        }

        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<OnlyTextSystemSystemUI>();
            var effectorData = (EffectorData) tileSystem.Data;
            var effect = (DefenceEffect) effectorData.Effect;
            OnlyTextSystemSystemViewModel viewModule = new(localizationSystem, effect.Description);

            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}