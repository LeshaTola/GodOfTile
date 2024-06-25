using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs
{
	public class GridConfig : ScriptableObject
	{
		[SerializeField] private Vector2Int gridSize;

		public Vector2Int GridSize { get => gridSize; set => gridSize = value; }
	}
}
