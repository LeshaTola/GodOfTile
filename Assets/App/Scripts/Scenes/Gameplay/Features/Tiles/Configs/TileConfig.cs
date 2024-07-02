using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Plate;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
	[CreateAssetMenu(fileName = "TileConfig", menuName = "Configs/Tiles/Tile")]
	public class TileConfig : ScriptableObject
	{
		[SerializeField]
		private Vector2Int size = Vector2Int.one;

		[SerializeField]
		private TileTypeDatabase tileTypeDatabase;

		[SerializeField]
		private GameObject building;

		[SerializeField]
		[ValueDropdown(nameof(GetTileTypes))]
		[ShowIf("@tileTypeDatabase !=null")]
		private string type;

		[PreviewField]
		[SerializeField]
		[FoldoutGroup("Extra Information")]
		private Sprite tileImage;

		[SerializeField]
		[FoldoutGroup("Extra Information")]
		private string tileName;

		[TextArea]
		[SerializeField]
		[FoldoutGroup("Extra Information")]
		private string description;

		public Vector2Int Size
		{
			get => size;
		}
		public string Type
		{
			get => type;
		}
		public Material TypeMaterial
		{
			get => tileTypeDatabase.Types[type];
		}
		public GameObject Building
		{
			get => building;
		}
		public Sprite TileSprite
		{
			get => tileImage;
		}
		public string Description
		{
			get => description;
		}
		public string Name
		{
			get => tileName;
		}

		private IEnumerable<string> GetTileTypes()
		{
			if (tileTypeDatabase == null)
			{
				return null;
			}

			return new List<string>(tileTypeDatabase.Types.Keys);
		}
	}
}
