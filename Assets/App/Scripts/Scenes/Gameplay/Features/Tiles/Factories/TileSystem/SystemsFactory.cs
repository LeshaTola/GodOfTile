using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem
{
    public class SystemsFactory : ISystemsFactory
    {
        private DiContainer diContainer;

        public SystemsFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public TileSystems.TileSystem GetSystem(TileSystems.TileSystem original,Tile parent)
        {
            var type = original.GetType();
            var newSystem = diContainer.Instantiate(type, new object[] {original.Data, parent});
            return (TileSystems.TileSystem) newSystem;
        }

        public List<TileSystems.TileSystem> GetSystems(List<TileSystems.TileSystem> originals,Tile parent)
        {
            List<TileSystems.TileSystem> tileSystems = new();
            foreach (var system in originals)
            {
                tileSystems.Add(GetSystem(system, parent));
            }

            return tileSystems;
        }
    }
}