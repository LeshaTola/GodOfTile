using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies.Lined
{
    public class LinedGetTilesStrategy : GetTileStrategy
    {
        [SerializeField] private List<Line> steps;
        
        public override List<Vector2Int> GetPositions(Vector2Int center)
        {
            if (steps == null || steps.Count <= 0)
            {
                return null;
            }
            
            List<Vector2Int> positions = new();
            for (var i = 0; i < steps.Count; i++)
            {
                var iteration = 1;
                while (true)
                {
                    var tilePosition = center + steps[i].Direction * iteration;

                    if (!IsValidPosition(tilePosition) || IsCompete(steps[i], iteration))
                    {
                        break;
                    }
                    
                    positions.Add(tilePosition);
                    iteration++;
                }
            }

            return positions;
        }
        
        
        private bool IsCompete(Line step, int iteration)
        {
            return step.iterations != -1 && iteration > step.iterations;
        }
    }
}