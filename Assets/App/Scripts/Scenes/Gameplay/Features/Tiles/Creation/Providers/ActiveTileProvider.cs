using System;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Providers
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