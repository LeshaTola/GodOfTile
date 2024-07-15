using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Shop.Configs
{
    [CreateAssetMenu(fileName = "CollectionConfig", menuName = "Configs/Collection")]
    public class CollectionConfig : ScriptableObject
    {
        [SerializeField]
        private List<TileConfig> startTiles;

        public List<TileConfig> StartTiles => startTiles;
    }
}