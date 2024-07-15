using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories
{
    public interface ITilesFactory
    {
        public Tile GetTile(string id);
        public Tile GetTile(TileConfig tileConfig);
    }
}
