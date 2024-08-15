using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems
{
    public abstract class TileSystemData
    {
        public abstract SystemUI TileUI { get; }
    }

    public abstract class TileSystem : ITileSystem
    {
        public abstract TileSystemData Data { get; }
        protected ISystemUIProvider SystemUIProvider;
        protected Tile ParentTile;

        public TileSystem(Tile parentTile)
        {
            ParentTile = parentTile;
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Stop()
        {
        }

        public SystemUI GetSystemUI()
        {
            return SystemUIProvider.GetSystemUI(this);
        }
    }
}