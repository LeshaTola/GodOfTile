namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems
{
    public class TileSystemData
    {
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
    }
}