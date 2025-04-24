using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update
{
    public interface ITilesUpdateService
    {
        public event Action<Vector2Int, Tile> OnTileUpdated;
        
        void UpdateConnectedTiles(Vector2Int tilePosition);
    }
}