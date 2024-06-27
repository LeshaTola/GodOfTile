using System;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace CraftingSystem
{
	[CreateAssetMenu(fileName = "Recipe", menuName = "Configs/CraftingSystem/Recipe")]
	internal class RecipeSO : ScriptableObject
	{
		[field: SerializeField] public List<Tile> RequiredTiles { get; private set; }

		[field: SerializeField] public Tile Original { get; private set; }
		[field: SerializeField] public Tile Result { get; private set; }
	}

	[Serializable]
	internal class TileWithCount
	{
		[field: SerializeField] public Tile Tile { get; private set; }
		[field: SerializeField] public int Count { get; private set; }
	}
}
