using App.Scripts.Modules.PopupAndViews.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Map.Items;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk.Cost;
using App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.ChunkFilling;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand
{
    public class BuyAreaCommand : LabeledCommand
    {
        public Vector2Int ChunkId { get; set; }

        private readonly IChunksProvider chunksProvider;
        private readonly IInventorySystem inventorySystem;
        private readonly IChunkCostProvider chunkCostProvider;
        private readonly IPopupController popupController;
        private readonly ChunkFillingService chunkFillingService;

        public BuyAreaCommand(string label, 
            IChunksProvider chunksProvider,
            IInventorySystem inventorySystem,
            IChunkCostProvider chunkCostProvider,
            IPopupController popupController, 
            ChunkFillingService chunkFillingService) : base(label)
        {
            this.chunksProvider = chunksProvider;
            this.inventorySystem = inventorySystem;
            this.chunkCostProvider = chunkCostProvider;
            this.popupController = popupController;
            this.chunkFillingService = chunkFillingService;
        }

        public override void Execute()
        {
            var cost = chunkCostProvider.GetCost(ChunkId);
            if (cost == null || !inventorySystem.IsEnough(cost))
            {
                return;
            }

            chunksProvider.OpenChunk(ChunkId);
            chunkFillingService.GenerateChankAsync(ChunkId).Forget();
            foreach (var resourceCount in cost)
            {
                inventorySystem.ChangeRecourseAmount(resourceCount.Resource.ResourceName, -resourceCount.Count);
            }

            popupController.HidePopup<BuyAreaPopup>().Forget();
        }
    }
}