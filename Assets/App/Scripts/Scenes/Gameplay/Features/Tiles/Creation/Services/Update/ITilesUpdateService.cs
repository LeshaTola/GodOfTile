using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Update
{
	public interface ITilesUpdateService
	{
		void UpdateConnectedTiles(Vector2Int tilePosition);
	}
}