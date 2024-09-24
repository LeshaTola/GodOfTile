using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk.Cost
{
    public class ChunkCostProvider : IChunkCostProvider
    {
        private ChunkCostConfig config;
        private IChunksProvider chunksProvider;

        public ChunkCostProvider(ChunkCostConfig config, IChunksProvider chunksProvider)
        {
            this.config = config;
            this.chunksProvider = chunksProvider;
        }

        public List<ResourceCount> GetCost(Vector2Int ChunkId)
        {
            var nextChunkId = chunksProvider.OpenedChunks.Count - 1;

            if (config.Costs == null || config.Costs.Count < nextChunkId)
            {
                return null;
            }

            return config.Costs[nextChunkId];
        }
    }
}