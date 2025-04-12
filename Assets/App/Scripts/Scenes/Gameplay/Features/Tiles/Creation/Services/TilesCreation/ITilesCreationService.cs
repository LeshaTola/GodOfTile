using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
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
        void PlaceActiveTile(bool isActive = true, bool withUpdate = false);
        void StartPlacingTile();
        void StopPlacingTile();
        MapState GetState();
        void SetState(MapState state);
        void PlaceTile(Vector2Int gridPosition, TileConfig tile, bool isActive, bool withUpdate);
        void DestroyTile(Vector2Int gridPosition);
    }
}