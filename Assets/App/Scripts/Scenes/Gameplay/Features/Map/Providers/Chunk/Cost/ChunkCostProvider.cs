using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Cost
{
    public class ChunkCostProvider : IChunkCostProvider
    {

        public List<ResourceCount> GetCost(Vector2Int ChunkId)
        {
            return new();
        }
    }
}