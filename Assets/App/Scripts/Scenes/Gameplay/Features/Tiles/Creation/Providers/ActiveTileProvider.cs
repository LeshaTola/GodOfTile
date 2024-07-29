using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers
{
    public class ActiveTileProvider : IActiveTileProvider
    {
        private TileConfig tileConfig;

        public TileConfig ActiveTileConfig
        {
            get => tileConfig;
            set
            {
                tileConfig = value;
                OnActiveTileChanged?.Invoke();
            }
        }

        public event Action OnActiveTileChanged;
    }
}