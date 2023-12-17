using UnityEngine;

namespace TileSystem
{
	[CreateAssetMenu(fileName = "TileSO", menuName = "TileSystem/TileSO")]
	public class TileSO : ScriptableObject
	{
		[field: SerializeField] public Sprite Sprite { get; private set; }
		[field: SerializeField] public Tile Prefab { get; private set; }
	}
}