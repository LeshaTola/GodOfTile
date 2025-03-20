using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
    [CreateAssetMenu(fileName = "TilesDatabase", menuName = "Databases/Tiles/Configs")]
    public class TilesDatabase : SerializedScriptableObject
    {
        [SerializeField] private List<TileConfig> configs;
        [SerializeField,ReadOnly] private Dictionary<string, TileConfig> configsDic;

        public Dictionary<string, TileConfig> Configs => configsDic;

        [Button]
        private void Validate()
        {
            var newConfigs = new Dictionary<string, TileConfig>() ;
            foreach (var tileConfig in configs)
            {
                newConfigs.Add(tileConfig.Id, tileConfig);
            }

            configsDic = newConfigs;
        }
    }
}