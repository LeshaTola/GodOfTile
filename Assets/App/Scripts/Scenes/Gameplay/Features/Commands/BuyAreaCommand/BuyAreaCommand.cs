using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Cost;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizators;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand
{
    public class BuyAreaCommand : LabeledCommand
    {
        public Vector2Int ChunkId { get; set; }

        private IChunksProvider chunksProvider;
        private IInventorySystem inventorySystem;
        private IChunkCostProvider chunkCostProvider;
       // private IMapVisualizator mapVisualizator;

        public BuyAreaCommand(string label, IChunksProvider chunksProvider, IInventorySystem inventorySystem,
            IChunkCostProvider chunkCostProvider/*,IMapVisualizator mapVisualizator*/) : base(label)
        {
            this.chunksProvider = chunksProvider;
            this.inventorySystem = inventorySystem;
            this.chunkCostProvider = chunkCostProvider;
           // this.mapVisualizator = mapVisualizator;
        }

        public override void Execute()
        {
            var cost = chunkCostProvider.GetCost(ChunkId);
            if (!inventorySystem.IsEnough(cost))
            {
                return;
            }

            chunksProvider.OpenChunk(ChunkId);
           // mapVisualizator.UpdateChunks();
            foreach (var resourceCount in cost)
            {
                inventorySystem.ChangeRecourseAmount(resourceCount.Resource.ResourceName, -resourceCount.Count);
            }
        }
    }
}