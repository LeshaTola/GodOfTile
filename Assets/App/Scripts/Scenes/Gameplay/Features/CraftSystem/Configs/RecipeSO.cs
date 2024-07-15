using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Configs/CraftingSystem/Recipe")]
    internal class RecipeSO : ScriptableObject
    {
        [field: SerializeField] public List<TileConfig> RequiredTiles { get; private set; }

        [field: SerializeField] public TileConfig Original { get; private set; }
        [field: SerializeField] public TileConfig Result { get; private set; }
    }

    [Serializable]
    internal class TileWithCount
    {
        [field: SerializeField] public Tile Tile { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}