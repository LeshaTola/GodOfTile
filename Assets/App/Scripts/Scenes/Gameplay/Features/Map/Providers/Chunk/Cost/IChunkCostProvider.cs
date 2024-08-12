using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Cost
{
    public interface IChunkCostProvider
    {
        List<ResourceCount> GetCost(Vector2Int ChunkId);
    }
}