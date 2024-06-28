using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services
{
    public interface ICreationService
    {
        void FullFill();
        void MoveActiveTile(Vector3 worldPosition);
        void PlaceActiveTile();
        void StartPlacingTile();
        void StopPlacingTile();
    }
}
