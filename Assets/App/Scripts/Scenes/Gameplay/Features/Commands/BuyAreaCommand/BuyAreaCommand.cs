using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk.Cost;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand
{
    public class BuyAreaCommand : LabeledCommand
    {
        public Vector2Int ChunkId { get; set; }

        private IChunksProvider chunksProvider;
        private IInventorySystem inventorySystem;
        private IChunkCostProvider chunkCostProvider;
        private IPopupController popupController;

        public BuyAreaCommand(string label, IChunksProvider chunksProvider, IInventorySystem inventorySystem,
            IChunkCostProvider chunkCostProvider, IPopupController popupController) : base(label)
        {
            this.chunksProvider = chunksProvider;
            this.inventorySystem = inventorySystem;
            this.chunkCostProvider = chunkCostProvider;
            this.popupController = popupController;
        }

        public override void Execute()
        {
            var cost = chunkCostProvider.GetCost(ChunkId);
            if (cost == null || !inventorySystem.IsEnough(cost))
            {
                return;
            }

            chunksProvider.OpenChunk(ChunkId);
            foreach (var resourceCount in cost)
            {
                inventorySystem.ChangeRecourseAmount(resourceCount.Resource.ResourceName, -resourceCount.Count);
            }

            popupController.HideLastPopup();
        }
    }
}