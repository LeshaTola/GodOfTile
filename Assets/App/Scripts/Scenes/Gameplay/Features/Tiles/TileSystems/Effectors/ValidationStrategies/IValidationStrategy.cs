using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public interface IValidationStrategy
    {
        List<Tile> ValidateTiles(List<Tile> tiles);
        List<TileSystem> GetValidSystems(List<Tile> tiles);
    }
}