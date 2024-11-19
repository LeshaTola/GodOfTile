using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection
{
    public class TileCollectionProvider : ITileCollectionProvider
    {
        private CollectionConfig config;
        private TilesDatabase tilesDatabase;

        public TileCollectionProvider(
            CollectionConfig config,
            TilesDatabase tilesDatabase)
        {
            this.config = config;
            this.tilesDatabase = tilesDatabase;

            foreach (var tile in config.StartTiles)
            {
                Collection.Add(tile);
            }
        }

        public List<TileConfig> Collection { get; } = new();

        public event Action<TileConfig> OnNewTileAdd;

        public void AddTile(TileConfig tileConfig)
        {
            Collection.Add(tileConfig);
            OnNewTileAdd?.Invoke(tileConfig);
        }

        public void AddIfNotContains(TileConfig tileConfig)
        {
            if (Collection
                    .FirstOrDefault(x => x.Id.Equals(tileConfig.Id)) == null)
            {
                AddTile(Object.Instantiate(tileConfig));
            }
        }

        public void AddIfNotContainsById(string id)
        {
            if (tilesDatabase.Configs.ContainsKey(id))
            {
                Collection.Add(tilesDatabase.Configs[id]);
                return;
            }
            Debug.LogWarning($"Can't add tile with id: {id}");
        }
    }
    
    
}