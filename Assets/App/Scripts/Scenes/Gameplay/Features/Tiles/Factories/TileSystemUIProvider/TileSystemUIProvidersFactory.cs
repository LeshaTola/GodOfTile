using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider
{
    public class TileSystemUIProvidersFactory : ITileSystemUIProvidersFactory
    {
        private DiContainer diContainer;

        public TileSystemUIProvidersFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public ISystemUIProvider GetSystemUIProvider(ISystemUIProvider provider)
        {
            if (provider == null)
            {
                return null;
            }
            return GetSystemUIProvider(provider.GetType());
        }

        public ISystemUIProvider GetSystemUIProvider(Type provider)
        {
            return (ISystemUIProvider) diContainer.Resolve(provider);
        }
    }
}