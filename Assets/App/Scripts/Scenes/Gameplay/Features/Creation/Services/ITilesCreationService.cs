using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services
{
	public interface ITilesCreationService
	{
		void FullFill();
		void MoveActiveTile(Vector3 worldPosition);
		UniTask RotateActiveTile();
		void PlaceActiveTile();
		void StartPlacingTile();
		void StopPlacingTile();
	}
}
