using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies
{
    public abstract class GetTileStrategy : IGetTilesStrategy
    {
        private IGridProvider gridProvider;

        public void Initialize(IGridProvider gridProvider)
        {
            this.gridProvider = gridProvider;
        }

        public virtual List<Tile> GetTiles(Vector2Int center)
        {
            var tilePositions = GetPositions(center);
            if (tilePositions == null || tilePositions.Count <= 0)
            {
                return null;
            }
            
            List<Tile> tiles = new();
            foreach (var tilePosition in tilePositions)
            {
                var neighbour = gridProvider.Grid[tilePosition.x, tilePosition.y];
                if (neighbour != null)
                {
                    tiles.Add(neighbour);
                }
            }

            return tiles;
        }

        public abstract List<Vector2Int> GetPositions(Vector2Int center);

        protected bool IsValidPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x <= gridProvider.GridSize.x &&
                   position.y >= 0 && position.y <= gridProvider.GridSize.y;
        }
    }
}