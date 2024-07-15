using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
    [CreateAssetMenu(fileName = "TilesDatabase", menuName = "Databases/Tiles/Configs")]
    public class TilesDatabase : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<string, TileConfig> configs;

        public Dictionary<string, TileConfig> Configs => configs;
    }
}