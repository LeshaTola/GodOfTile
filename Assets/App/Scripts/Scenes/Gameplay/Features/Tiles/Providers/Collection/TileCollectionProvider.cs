using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection
{
    public class TileCollectionProvider : ITileCollectionProvider
    {
        private CollectionConfig config;

        public TileCollectionProvider(CollectionConfig config)
        {
            this.config = config;

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
                AddTile(tileConfig);
            }
        }
    }
}