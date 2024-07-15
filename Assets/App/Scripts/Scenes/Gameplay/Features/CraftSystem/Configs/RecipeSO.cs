using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.CraftSystem.Configs
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