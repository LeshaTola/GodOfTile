using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems
{
    public abstract class TileSystemData
    {
        public abstract ResourceEarnerSystemUI TileSystemUI { get; }
    }

    public abstract class TileSystem : ITileSystem
    {
        public abstract TileSystemData Data { get; }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Stop()
        {
        }

        public abstract SystemUI GetSystemUI();
    }
}