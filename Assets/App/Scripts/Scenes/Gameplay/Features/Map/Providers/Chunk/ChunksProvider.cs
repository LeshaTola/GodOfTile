using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers
{
    public class ChunksProvider : IChunksProvider
    {
        private GridConfig config;

        public ChunksProvider(GridConfig config)
        {
            this.config = config;
            InitializeChunks();
        }

        public HashSet<Chunk> OpenedChunks { get; private set; }
        public HashSet<Chunk> ClosedChunks { get; private set;}

        public void OpenChunk(Vector2Int chunkId)
        {
            var chunk =  ClosedChunks.FirstOrDefault(x => x.Id.Equals(chunkId));
            if (chunk == null)
            {
                return;
            }

            OpenChunk(chunk);
        }
        
        public void OpenChunk(Chunk chunk)
        {
            if (!ClosedChunks.Contains(chunk))
            {
                return;
            }
            OpenedChunks.Add(chunk);
            ClosedChunks.Remove(chunk);
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

                if (endX <= position.x || endY <= position.y || position.x < startX || position.y < startY)
                {
                    return false;
                }
            }

            return true;
        }

        private void InitializeChunks()
        {
            OpenedChunks = new HashSet<Chunk>();
            ClosedChunks = new HashSet<Chunk>();

            for (int i = 0; i < config.ChunkSize.x; i++)
            {
                for (int j = 0; j < config.ChunkSize.y; j++)
                {
                    var id = new Vector2Int(i, j);
                    ClosedChunks.Add(new Chunk(id, config.ChunkSize));
                }
            }
            
            OpenChunk(config.StartChunk);
        } 
        
        private bool IsInOpenedChunk(int x, int y)
        {
            return IsInOpenedChunk(new Vector2Int(x, y));
        }
    }
}