using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.Saves.Structs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Saves
{
    public class GameplaySavesController
    {
        private IInventorySystem inventorySystem;
        private ITilesCreationService tilesCreationService;
        private IChunksProvider chunksProvider;
        private ITileCollectionProvider tileCollectionProvider;
        private IDataProvider<GamePlaySavesData> dataProvider;

        public GameplaySavesController(IInventorySystem inventorySystem,
            ITilesCreationService tilesCreationService,
            IChunksProvider chunksProvider,
            ITileCollectionProvider tileCollectionProvider,
            IDataProvider<GamePlaySavesData> dataProvider)
        {
            this.inventorySystem = inventorySystem;
            this.tilesCreationService = tilesCreationService;
            this.chunksProvider = chunksProvider;
            this.tileCollectionProvider = tileCollectionProvider;
            this.dataProvider = dataProvider;
        }

        public void Save()
        {
            dataProvider.SaveData(new()
            {
                InventoryState = inventorySystem.GetState(),
                MapState = tilesCreationService.GetState(),
                OpenedChunk = chunksProvider.OpenedChunks.Select(x=>new JsonVector2Int(x.Id)).ToList(),
                Collection = tileCollectionProvider.Collection.Select(x=>x.Id).ToList()
            });
        }

        public void Load()
        {
            if (!dataProvider.HasData())
            {
                var data = GetDefaultData();
                dataProvider.SaveData(data);
            }

            var loadedData = dataProvider.GetData();
            
            inventorySystem.SetState(loadedData.InventoryState);
            tilesCreationService.SetState(loadedData.MapState);

            foreach (var chunkId in loadedData.OpenedChunk)
            {
                chunksProvider.OpenChunk(new Vector2Int(chunkId.X, chunkId.Y));
            }

            foreach (var id in loadedData.Collection)
            {
                tileCollectionProvider.AddIfNotContainsById(id);
            }
        }

        private GamePlaySavesData GetDefaultData()
        {
            return new()
            {
                InventoryState = new()
                {
                    Resources = new()
                },
                MapState = new()
                {
                    Grid = new(),
                },
                OpenedChunk = new(),
                Collection = new(),
            };
        }
    }

    public class GamePlaySavesData
    {
        public InventoryState InventoryState;
        public MapState MapState;
        public List<JsonVector2Int> OpenedChunk;
        public List<string> Collection;

    }
}