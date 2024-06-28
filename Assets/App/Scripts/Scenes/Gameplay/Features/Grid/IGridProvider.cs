using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Grid
{
	public interface IGridProvider
	{
		Vector2Int GridSize { get; }
		Tile[,] Grid { get; }

		List<Vector2Int> GetCoveringTiles(Vector2Int tile);

		bool IsValid(Vector2Int position);
		bool IsValid(Tile tile);
		bool IsInsideGrid(Tile tile);
	}
}
