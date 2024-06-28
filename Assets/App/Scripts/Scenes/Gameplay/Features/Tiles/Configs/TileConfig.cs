using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{

	[CreateAssetMenu(fileName = "TileConfig", menuName = "Configs/Tiles/Tile")]
	public class TileConfig : ScriptableObject
	{
		[SerializeField] private Vector2Int size = Vector2Int.one;

		public Vector2Int Size { get => size; }
	}
}
