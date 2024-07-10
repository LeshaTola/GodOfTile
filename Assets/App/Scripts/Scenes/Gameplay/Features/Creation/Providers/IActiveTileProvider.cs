using System;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Providers
{
    public interface IActiveTileProvider
    {
        event Action OnActiveTileChanged;
        TileConfig ActiveTileConfig { get; set; }
    }
}
