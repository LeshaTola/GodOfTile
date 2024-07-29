using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation
{
    public interface ITilesCreationService
    {
        void FullFill();
        void MoveActiveTile(Vector2Int gridPosition);
        UniTask RotateActiveTile();
        void PlaceActiveTile();
        void StartPlacingTile();
        void StopPlacingTile();
    }
}