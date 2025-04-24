using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.Saves.Structs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;
using App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Saves
{
    public class GameplaySavesController
    {
        private readonly IInventorySystem inventorySystem;
        private readonly ITilesCreationService tilesCreationService;
        private readonly IChunksProvider chunksProvider;
        private readonly ITileCollectionProvider tileCollectionProvider;
        
        private readonly IDataProvider<GamePlaySavesData> dataProvider;
        private readonly CataclysmsProvider cataclysmsProvider;
        private readonly IResearchService researchService;

        public GameplaySavesController(IInventorySystem inventorySystem,
            ITilesCreationService tilesCreationService,
            IChunksProvider chunksProvider,
            ITileCollectionProvider tileCollectionProvider,
            IDataProvider<GamePlaySavesData> dataProvider,
            CataclysmsProvider cataclysmsProvider,
            IResearchService researchService)
        {
            this.inventorySystem = inventorySystem;
            this.tilesCreationService = tilesCreationService;
            this.chunksProvider = chunksProvider;
            this.tileCollectionProvider = tileCollectionProvider;
            this.dataProvider = dataProvider;
            this.cataclysmsProvider = cataclysmsProvider;
            this.researchService = researchService;
        }

        public void Save()
        {
            dataProvider.SaveData(new()
            {
                InventoryState = inventorySystem.GetState(),
                MapState = tilesCreationService.GetState(),
                OpenedChunk = chunksProvider.OpenedChunks.Select(x=>new JsonVector2Int(x.Id)).ToList(),
                Collection = tileCollectionProvider.Collection.Select(x=>x.Id).ToList(),
                CataclysmTimer = cataclysmsProvider.Timer,
                ResearchState = researchService.GetState(),
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
            foreach (var chunkId in loadedData.OpenedChunk)
            {
                chunksProvider.OpenChunk(new Vector2Int(chunkId.X, chunkId.Y));
            }
            tilesCreationService.SetState(loadedData.MapState);


            foreach (var id in loadedData.Collection)
            {
                tileCollectionProvider.AddIfNotContainsById(id);
            }
            
            cataclysmsProvider.Timer = loadedData.CataclysmTimer;
            
            researchService.SetState(loadedData.ResearchState);
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
                CataclysmTimer = 0,
                ResearchState = new()
            };
        }
    }

    public class GamePlaySavesData
    {
        public InventoryState InventoryState;
        public MapState MapState;
        public ResearchState ResearchState;
        public List<JsonVector2Int> OpenedChunk;
        public List<string> Collection;
        public float CataclysmTimer;

    }
}