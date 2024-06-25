using TileSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Grid
{
	public interface IGridProvider
	{
		Vector2Int GridSize { get; }
		Tile[,] Grid { get; }

		bool IsValid(Vector2Int position);
		bool IsValid(Vector2Int bottomLeft, Vector2Int topRight);
	}
}
