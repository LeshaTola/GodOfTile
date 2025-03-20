using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk
{
    public class ChunksProvider : IChunksProvider
    {
        public event Action<Vector2Int> OnChunkOpened;

        private GridConfig config;

        private List<Vector2Int> neigboursDirection = new()
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };

        public ChunksProvider(GridConfig config)
        {
            this.config = config;
            InitializeChunks();
        }

        public HashSet<Items.Chunk> OpenedChunks { get; private set; }
        public HashSet<Items.Chunk> ClosedChunks { get; private set; }

        public void OpenChunk(Vector2Int chunkId)
        {
            var chunk = GetChunkById(chunkId);
            if (chunk == null)
            {
                return;
            }

            OpenChunk(chunk);
            OnChunkOpened?.Invoke(chunkId);
        }

        public void OpenChunk(Items.Chunk chunk)
        {
            if (!ClosedChunks.Contains(chunk))
            {
                return;
            }

            OpenedChunks.Add(chunk);
            ClosedChunks.Remove(chunk);
        }

        public bool IsOpenedNeighbour(Vector2Int id)
        {
            foreach (var openedChunk in OpenedChunks)
            {
                foreach (var direction in neigboursDirection)
                {
                    var neighbourId = openedChunk.Id + direction;
                    if (neighbourId.Equals(id))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsInOpenedChunk(Tile tile)
        {
            for (var x = tile.Position.x; x < tile.Position.x + tile.Config.Size.x; x++)
            {
                for (var y = tile.Position.y; y < tile.Position.y + tile.Config.Size.y; y++)
                {
                    if (!IsInOpenedChunk(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsInOpenedChunk(Vector2Int position)
        {
            foreach (var chunk in OpenedChunks)
            {
                var startX = chunk.Id.x * config.ChunkSize.x;
                var startY = chunk.Id.y * config.ChunkSize.y;

                var endX = (chunk.Id.x + 1) * config.ChunkSize.x;
                var endY = (chunk.Id.y + 1) * config.ChunkSize.y;

                if (position.x >= startX && position.y >= startY && position.x < endX && position.y < endY)
                {
                    return true;
                }
            }

            return false;
        }

        private void InitializeChunks()
        {
            OpenedChunks = new HashSet<Items.Chunk>();
            ClosedChunks = new HashSet<Items.Chunk>();

            for (var i = 0; i < config.ChunksCount.x; i++)
            {
                for (var j = 0; j < config.ChunksCount.y; j++)
                {
                    var id = new Vector2Int(i, j);
                    ClosedChunks.Add(new Items.Chunk(id, config.ChunkSize));
                }
            }

            OpenChunk(config.StartChunk);
        }

        private bool IsInOpenedChunk(int x, int y)
        {
            return IsInOpenedChunk(new Vector2Int(x, y));
        }

        private Items.Chunk GetChunkById(Vector2Int chunkId)
        {
            var chunk = ClosedChunks.FirstOrDefault(x => x.Id.Equals(chunkId));
            return chunk;
        }
    }
}