using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies
{
    public class LinedGetTilesStrategy : IGetTilesStrategy
    {
        [SerializeField] private List<Line> steps;

        private IGridProvider gridProvider;

        public void Initialize(IGridProvider gridProvider)
        {
            this.gridProvider = gridProvider;
        }
        
        public List<Tile> GetTiles(Vector2Int center)
        {
            if (steps == null || steps.Count <= 0)
            {
                return null;
            }

            if (gridProvider == null)
            {
                Debug.LogWarning("gridProvider is null");
                return null;
            }


            return GetValidTiles(center);
        }

        private List<Tile> GetValidTiles(Vector2Int center)
        {
            List<Tile> tiles = new();

            for (var i = 0; i < steps.Count; i++)
            {
                var iteration = 1;
                while (true)
                {
                    var tilePosition = center + steps[i].Direction * iteration;

                    if (!IsValidPosition(tilePosition, gridProvider.GridSize) || IsCompete(steps[i], iteration))
                    {
                        break;
                    }

                    var neighbour = gridProvider.Grid[tilePosition.x, tilePosition.y];

                    if (neighbour != null)
                    {
                        tiles.Add(neighbour);
                    }

                    iteration++;
                }
            }

            return tiles;
        }

        private bool IsCompete(Line step, int iteration)
        {
            return step.iterations != -1 && iteration > step.iterations;
        }

        private bool IsValidPosition(Vector2Int position, Vector2Int matrixSize)
        {
            return position.x >= 0 && position.x <= matrixSize.x &&
                   position.y >= 0 && position.y <= matrixSize.y;
        }
    }
}