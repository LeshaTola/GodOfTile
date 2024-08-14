using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid
{
    public class GridProvider : IGridProvider
    {
        private GridConfig config;
        private IChunksProvider chunksProvider;

        public GridProvider(GridConfig config, IChunksProvider chunksProvider)
        {
            this.config = config;
            this.chunksProvider = chunksProvider;
            GridSize = config.ChunkSize * config.ChunksCount;
            Grid = new Tile[GridSize.x, GridSize.y];

        }

        public Vector2Int GridSize { get; }
        public Tile[,] Grid { get; }

        public List<Vector2Int> GetCoveringTiles(Vector2Int tile)
        {
            var neighbors = new List<Vector2Int>();
            for (var x = tile.x - 1; x <= tile.x + 1; x++)
            {
                for (var y = tile.y - 1; y <= tile.y + 1; y++)
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
            if (Grid == null || !IsInsideGrid(position) || !chunksProvider.IsInOpenedChunk(position))
            {
                return false;
            }

            return Grid[position.x, position.y] != null;
        }

        public bool IsValid(Tile tile)
        {
            if (Grid == null || !IsInsideGrid(tile) || !chunksProvider.IsInOpenedChunk(tile))
            {
                return false;
            }

            for (var x = tile.Position.x; x < tile.Position.x + tile.Config.Size.x; x++)
            {
                for (var y = tile.Position.y; y < tile.Position.y + tile.Config.Size.y; y++)
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
            for (var x = tile.Position.x; x < tile.Position.x + tile.Config.Size.x; x++)
            {
                for (var y = tile.Position.y; y < tile.Position.y + tile.Config.Size.y; y++)
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