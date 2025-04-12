using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.ChunkFilling
{
    public class ChunkFillingService
    {
        private readonly ITilesCreationService tilesCreationService;
        private readonly TilesDatabase tilesDatabase;
        private readonly IGridProvider gridProvider;
        
        private readonly int millisecondsDelay = 10; //TODO in config

        public ChunkFillingService(ITilesCreationService tilesCreationService,
            TilesDatabase tilesDatabase,
            IGridProvider gridProvider)
        {
            this.tilesCreationService = tilesCreationService;
            this.tilesDatabase = tilesDatabase;
            this.gridProvider = gridProvider;
        }

        public void GenerateChank(Configs.ChunkFilling chunkFilling)
        {
            var startPos = gridProvider.GridSize * chunkFilling.ChunkId;
            
            foreach (var tile in chunkFilling.Tiles)
            {
                PlaceTile(tile, startPos);
            }
        }
        
        public async UniTask GenerateChankAsync(Configs.ChunkFilling chunkFilling)
        {
            var startPos = gridProvider.GridSize * chunkFilling.ChunkId;
            
            foreach (var tile in chunkFilling.Tiles)
            {
                await UniTask.Delay(millisecondsDelay);
                PlaceTile(tile, startPos);
            }
        }

        private void PlaceTile(TileWithPosition tile, Vector2Int startPos)
        {
            if (tilesDatabase.Configs.TryGetValue(tile.TileId, out var config))
            {
                var position = startPos + tile.Position;
                tilesCreationService.PlaceTile(position, config, tile.IsActive, false);
            }
        }
    }
}