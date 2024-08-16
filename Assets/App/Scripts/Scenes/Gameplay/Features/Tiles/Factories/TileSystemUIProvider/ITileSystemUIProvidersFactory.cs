using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.UIProvidersFactory
{
    public interface ITileSystemUIProvidersFactory
    {
        ISystemUIProvider GetSystemUIProvider(ISystemUIProvider provider);
        ISystemUIProvider GetSystemUIProvider(Type provider);
    }
}