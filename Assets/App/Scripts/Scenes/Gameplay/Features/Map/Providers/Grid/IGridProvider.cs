using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid
{
    public interface IGridProvider
    {
        Vector2Int GridSize { get; }
        Tile[,] Grid { get; }

        List<Vector2Int> GetCoveringTiles(Vector2Int tile);

        bool IsValid(Vector2Int position);
        bool IsValid(Tile tile);
        bool IsInsideGrid(Tile tile);
    }
}