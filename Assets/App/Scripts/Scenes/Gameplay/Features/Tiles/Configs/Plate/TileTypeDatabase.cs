using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Plate
{
	[CreateAssetMenu(fileName = "TileTypeDatabase", menuName = "Databases/Tiles/Types")]
	public class TileTypeDatabase : SerializedScriptableObject
	{
		[SerializeField]
		private Dictionary<string, Material> types;

		public Dictionary<string, Material> Types
		{
			get => types;
		}
	}
}
