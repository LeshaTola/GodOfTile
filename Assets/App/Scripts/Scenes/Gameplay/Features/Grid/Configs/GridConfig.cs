using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs
{

	[CreateAssetMenu(fileName = "GridConfig", menuName = "Configs/Grid")]
	public class GridConfig : ScriptableObject
	{
		[SerializeField] private Vector2Int gridSize;

		public Vector2Int GridSize { get => gridSize; set => gridSize = value; }
	}
}
