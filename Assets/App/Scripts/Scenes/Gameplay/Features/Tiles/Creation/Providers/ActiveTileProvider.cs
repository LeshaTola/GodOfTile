using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers
{
    public class ActiveTileProvider : IActiveTileProvider
    {
        private TilesDatabase tilesDatabase;
        private TileConfig tileConfig;

        public ActiveTileProvider(TilesDatabase tilesDatabase)
        {
            this.tilesDatabase = tilesDatabase;
        }

        public TileConfig ActiveTileConfig
        {
            get => tileConfig;
            set
            {
                tileConfig = value;
                OnActiveTileChanged?.Invoke();
            }
        }

        public void SetActiveTileByID(string id)
        {
            if (tilesDatabase.Configs.ContainsKey(id))
            {
                ActiveTileConfig = tilesDatabase.Configs[id];
                return;
            }

            Debug.LogWarning($"Add config with id {id}");
        }

        public event Action OnActiveTileChanged;
    }
}