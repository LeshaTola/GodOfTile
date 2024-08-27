using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies.Lined
{
    public struct Line
    {
        public Vector2Int Direction;
        [Min(-1)] public int iterations;
    }
}