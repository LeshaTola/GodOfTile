using System.Collections.Generic;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Factories
{
    public class SystemsFactory : ISystemsFactory
    {
        private DiContainer diContainer;


        public SystemsFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public TileSystem GetSystem(TileSystem original)
        {
            var type = original.GetType();
            var newSystem = diContainer.Instantiate(type, new object[] {original.Data});
            return (TileSystems.TileSystem) newSystem;
        }

        public List<TileSystem> GetSystems(List<TileSystem> originals)
        {
            List<TileSystem> tileSystems = new();
            foreach (var system in originals)
            {
                tileSystems.Add(GetSystem(system));
            }

            return tileSystems;
        }
    }
}