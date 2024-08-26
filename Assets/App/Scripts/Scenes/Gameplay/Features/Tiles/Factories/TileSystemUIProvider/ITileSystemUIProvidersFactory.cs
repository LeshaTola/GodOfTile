using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider
{
    public interface ITileSystemUIProvidersFactory
    {
        ISystemUIProvider GetSystemUIProvider(ISystemUIProvider provider);
        ISystemUIProvider GetSystemUIProvider(Type provider);
    }
}