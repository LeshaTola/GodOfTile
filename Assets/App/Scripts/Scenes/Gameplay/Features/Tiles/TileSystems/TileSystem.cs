using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems
{
    public abstract class TileSystemData
    {
        public abstract SystemUI TileUI { get; }
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