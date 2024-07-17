using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI
{
    public interface ISystemUI
    {
        public void Init(TileSystem system, ILocalizationSystem localizationSystem);
        public void Translate();
        public void Cleanup();
    }
}