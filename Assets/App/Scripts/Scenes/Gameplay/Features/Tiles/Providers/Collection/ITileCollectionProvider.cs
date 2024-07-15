using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection
{
    public interface ITileCollectionProvider
    {
        List<TileConfig> Collection { get; }

        event Action<TileConfig> OnNewTileAdd;
    }
}