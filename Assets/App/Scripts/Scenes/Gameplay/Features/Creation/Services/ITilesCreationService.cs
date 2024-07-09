using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services
{
    public interface ITilesCreationService
    {
        event Action OnTilePlaced;

        void FullFill();
        void MoveActiveTile(Vector2Int gridPosition);
        UniTask RotateActiveTile();
        void PlaceActiveTile();
        void StartPlacingTile();
        void StopPlacingTile();
    }
}
