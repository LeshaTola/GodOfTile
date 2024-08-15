using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk.Cost
{
    public interface IChunkCostProvider
    {
        List<ResourceCount> GetCost(Vector2Int ChunkId);
    }
}