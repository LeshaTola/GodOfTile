using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies
{
    public interface IGetTilesStrategy
    {
        void Initialize(IGridProvider gridProvider);
        List<Tile> GetTiles(Vector2Int center);
        List<Vector2Int> GetPositions(Vector2Int center);
        bool IsValid(Vector2Int center, Vector2Int position);
    }
}