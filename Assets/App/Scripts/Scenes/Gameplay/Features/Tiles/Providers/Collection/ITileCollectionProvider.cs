using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection
{
    public interface ITileCollectionProvider
    {
        List<TileConfig> Collection { get; }

        event Action<TileConfig> OnNewTileAdd;
    }
}