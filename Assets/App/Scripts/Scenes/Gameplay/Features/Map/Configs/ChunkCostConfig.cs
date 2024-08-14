using System.Collections.Generic;
using App.Scripts.Modules.MinMaxValue;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Configs
{
    [CreateAssetMenu(fileName = "ChunkCostConfig", menuName = "Configs/Map/ChunkCost")]
    public class ChunkCostConfig : SerializedScriptableObject
    {
        [FoldoutGroup("Settings")]
        [SerializeField]
        private int levelsToGenerate;

        [FoldoutGroup("Settings")]
        [SerializeField]
        private MinMaxInt values;

        [FoldoutGroup("Settings")]
        [SerializeField]
        private MinMaxInt stepOffset;

        [FoldoutGroup("Settings")]
        [SerializeField]
        private ResourcesDatabase database;

        [FoldoutGroup("Settings")]
        [Button]
        public void GenerateLevels()
        {
            costs.Clear();
            var step = (values.Max - values.Min) / levelsToGenerate;

            for (var i = 1; i <= levelsToGenerate; i++)
            {
                var newLevel = new List<ResourceCount>();
                foreach (var resource in database.Resources)
                {
                    newLevel.Add(new ResourceCount()
                    {
                        Resource = resource,
                        Count = i * (step + stepOffset.GetRandom())
                    });
                }

                costs.Add(newLevel);
            }
        }

        [SerializeField] private List<List<ResourceCount>> costs;

        public List<List<ResourceCount>> Costs => costs;
    }
}