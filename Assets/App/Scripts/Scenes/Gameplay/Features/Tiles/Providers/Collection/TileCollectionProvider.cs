using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection
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

        public List<TileConfig> Collection { get; private set; } = new();

        public event Action<TileConfig> OnNewTileAdd;

        public void AddTile(TileConfig tileConfig)
        {
            Collection.Add(tileConfig);
            OnNewTileAdd?.Invoke(tileConfig);
        }
    }
}
