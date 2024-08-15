using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General
{
    public interface ISystemUIProvider
    {
        public SystemUI GetSystemUI(TileSystem tileSystem);
    }
}