using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public class NoValidationStrategy : IValidationStrategy
    {
        public List<Tile> ValidateTiles(List<Tile> tiles)
        {
            return tiles;
        }

        public List<TileSystem> GetValidSystems(List<Tile> tiles)
        {
            return tiles.SelectMany(tile =>tile.Config.ActiveSystems).ToList();
            ;
        }
    }
}