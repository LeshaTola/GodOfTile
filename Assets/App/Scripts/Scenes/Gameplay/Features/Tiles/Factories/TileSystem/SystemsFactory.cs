using System.Collections.Generic;
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

        public TileSystems.TileSystem GetSystem(TileSystems.TileSystem original)
        {
            var type = original.GetType();
            var newSystem = diContainer.Instantiate(type, new object[] {original.Data});
            return (TileSystems.TileSystem) newSystem;
        }

        public List<TileSystems.TileSystem> GetSystems(List<TileSystems.TileSystem> originals)
        {
            List<TileSystems.TileSystem> tileSystems = new();
            foreach (var system in originals)
            {
                tileSystems.Add(GetSystem(system));
            }

            return tileSystems;
        }
    }
}