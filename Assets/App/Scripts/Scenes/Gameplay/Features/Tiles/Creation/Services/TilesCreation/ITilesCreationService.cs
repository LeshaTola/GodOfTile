using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation
{
    public interface ITilesCreationService
    {
        event Action<Vector2Int, Tile> OnTilePlaced;

        void MoveActiveTile(Vector2Int gridPosition);
        UniTask RotateActiveTile();
        void PlaceActiveTile();
        void StartPlacingTile();
        void StopPlacingTile();
        MapState GetState();
        void SetState(MapState state);
    }
}