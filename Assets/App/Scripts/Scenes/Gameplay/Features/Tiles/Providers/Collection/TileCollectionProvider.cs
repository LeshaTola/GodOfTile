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
                AddIfNotContains(tile);
            }
        }

        public List<TileConfig> Collection { get; } = new();

        public event Action<TileConfig> OnNewTileAdd;

        public void AddIfNotContains(TileConfig tileConfig)
        {
            if (Collection
                    .FirstOrDefault(x => x.Id.Equals(tileConfig.Id)) == null)
            {
                AddTile(tileConfig);
            }
        }

        public void AddIfNotContainsById(string id)
        {
            if (tilesDatabase.Configs.TryGetValue(id, out var tileConfig))
            {
                AddIfNotContains(tileConfig);
                return;
            }
            Debug.LogWarning($"Can't add tile with id: {id}");
        }

        private void AddTile(TileConfig tileConfig)
        {
            Collection.Add(Object.Instantiate(tileConfig));
            OnNewTileAdd?.Invoke(tileConfig);
        }
    }
    
    
}