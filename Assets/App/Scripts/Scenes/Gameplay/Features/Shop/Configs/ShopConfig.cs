using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Configs
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "Configs/Shop")]
    public class ShopConfig : ScriptableObject
    {
        [SerializeField]
        private List<TileConfig> startTiles;

        public List<TileConfig> StartTiles
        {
            get => startTiles;
        }
    }
}
