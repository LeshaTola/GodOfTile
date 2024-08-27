using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies.Radius
{
    public class RadiusGetTileStrategy : GetTileStrategy
    {
        [SerializeField] private int radius;

        public override List<Vector2Int> GetPositions(Vector2Int center)
        {
            List<Vector2Int> positions = new List<Vector2Int>();

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Vector2Int point = new Vector2Int(center.x + x, center.y + y);

                    if (x * x + y * y <= radius * radius && IsValidPosition(point))
                    {
                        positions.Add(point);
                    }
                }
            }

            return positions;
        }
    }
}