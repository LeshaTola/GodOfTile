using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection
{
    public interface ITileSelectionProvider
    {
        Tile GetTileAtMousePosition();
    }
}