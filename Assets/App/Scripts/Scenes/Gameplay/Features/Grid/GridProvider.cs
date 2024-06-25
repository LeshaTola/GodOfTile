using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using TileSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Grid
{
	public class GridProvider : IGridProvider
	{
		public GridProvider(GridConfig config)
		{
			GridSize = config.GridSize;
			Grid = new Tile[GridSize.x, GridSize.y];
		}

		public Vector2Int GridSize { get; private set; }
		public Tile[,] Grid { get; private set; }

		public bool IsValid(Vector2Int position)
		{
			if (Grid == null || !IsInsideGrid(position))
			{
				return false;
			}

			return Grid[position.x, position.y] == null;
		}

		public bool IsValid(Vector2Int bottomLeft, Vector2Int topRight)
		{
			for (int x = bottomLeft.x; x < topRight.x; x++)
			{
				for (int y = bottomLeft.y; y < topRight.y; y++)
				{
					bool result = IsValid(new Vector2Int(x, y));
					if (!result)
					{
						return false;
					}
				}
			}

			return true;
		}

		private bool IsInsideGrid(Vector2Int position)
		{
			return GridSize.x >= position.x && GridSize.y >= position.y
				&& position.x >= 0 && position.y >= 0;
		}
	}
}
