using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.Tiles
{
    public interface ITilesFactory
    {
        public Tile GetTile(string id);
        public Tile GetTile(TileConfig tileConfig);
    }
}