using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies
{
    public interface IGetTilesStrategy
    {
        List<Tile> GetTiles(Vector2Int center);
        void Initialize(IGridProvider gridProvider);
    }
}