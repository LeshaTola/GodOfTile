using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers
{
    public interface IActiveTileProvider
    {
        event Action OnActiveTileChanged;
        TileConfig ActiveTileConfig { get; set; }

        void SetActiveTileByID(string id);
    }
}