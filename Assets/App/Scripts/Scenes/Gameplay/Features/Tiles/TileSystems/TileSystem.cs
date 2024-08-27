using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems
{
    public abstract class TileSystemData
    {
        public abstract ISystemUIProvider SystemUIProvider { get; }
    }

    public abstract class TileSystem : ITileSystem
    {
        private List<IEffect> effectors = new();

        public abstract TileSystemData Data { get; }
        public Tile ParentTile { get; }
        public IReadOnlyCollection<IEffect> Effectors => effectors;

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

        public void AddEffect(IEffect effect)
        {
            effectors.Add(effect);
            effect.AddEffect(Data);
        }

        public void RemoveEffect(IEffect effect)
        {
            effectors.Remove(effect);
            effect.RemoveEffect(Data);
        }
    }
}