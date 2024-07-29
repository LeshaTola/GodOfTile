using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI
{
    public interface ISystemUIFactory
    {
        SystemUI GetSystemUI(TileSystems.TileSystem tileSystem);
    }
}