using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories
{
    public interface ITilesFactory
    {
        public Tile GetTile(string id);
    }
}
