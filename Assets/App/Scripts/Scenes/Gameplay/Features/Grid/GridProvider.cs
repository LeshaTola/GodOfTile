using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Grid
{
    public class GridProvider : IGridProvider
    {
        public GridProvider(GridConfig config)
        {
            GridSize = config.GridSize;
            Grid = new Tile[GridSize.x, GridSize.y];
        }

        public Vector2Int GridSize { get; private set; }
        public Tile[,] Grid { get; private set; }

        public List<Vector2Int> GetCoveringTiles(Vector2Int tile)
        {
            var neighbors = new List<Vector2Int>();
            for (int x = tile.x - 1; x <= tile.x + 1; x++)
            {
                for (int y = tile.y - 1; y <= tile.y + 1; y++)
                {
                    if (!IsInsideGrid(x, y) || x == tile.x && y == tile.y)
                    {
                        continue;
                    }

                    neighbors.Add(new Vector2Int(x, y));
                }
            }
            return neighbors;
        }

        public bool IsValid(Vector2Int position)
        {
            if (Grid == null || !IsInsideGrid(position))
            {
                return false;
            }

            return Grid[position.x, position.y] != null;
        }

        public bool IsValid(Tile tile)
        {
            if (Grid == null || !IsInsideGrid(tile))
            {
                return false;
            }

            for (int x = tile.Position.x; x < tile.Position.x + tile.Config.Size.x; x++)
            {
                for (int y = tile.Position.y; y < tile.Position.y + tile.Config.Size.y; y++)
                {
                    if (Grid[x, y] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsInsideGrid(Tile tile)
        {
            for (int x = tile.Position.x; x < tile.Position.x + tile.Config.Size.x; x++)
            {
                for (int y = tile.Position.y; y < tile.Position.y + tile.Config.Size.y; y++)
                {
                    if (!IsInsideGrid(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsInsideGrid(int x, int y)
        {
            return IsInsideGrid(new Vector2Int(x, y));
        }

        private bool IsInsideGrid(Vector2Int position)
        {
            return GridSize.x > position.x
                && GridSize.y > position.y
                && position.x >= 0
                && position.y >= 0;
        }
    }
}
