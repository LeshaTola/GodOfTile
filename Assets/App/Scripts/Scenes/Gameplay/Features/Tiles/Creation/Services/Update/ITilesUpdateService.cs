using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update
{
    public interface ITilesUpdateService
    {
        void UpdateConnectedTiles(Vector2Int tilePosition);
    }
}