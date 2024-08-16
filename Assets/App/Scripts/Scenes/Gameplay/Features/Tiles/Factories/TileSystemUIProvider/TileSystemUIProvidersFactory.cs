using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.UIProvidersFactory
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
           return GetSystemUIProvider(provider.GetType());
        }

        public ISystemUIProvider GetSystemUIProvider(Type provider)
        {
            return (ISystemUIProvider)diContainer.Resolve(provider);
        }
    }
}