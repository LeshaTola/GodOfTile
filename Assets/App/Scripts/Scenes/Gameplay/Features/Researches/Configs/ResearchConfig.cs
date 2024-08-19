using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Researches.Commands;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Configs
{
    [CreateAssetMenu(fileName = "ResearchConfig", menuName = "Configs/Research/Research")]
    public class ResearchConfig : SerializedScriptableObject
    {
        [field: PreviewField]
        [field: SerializeField]
        [field: FoldoutGroup("Extra Information")]
        public Sprite ResearchImage { get; private set; }

        [field: SerializeField]
        [field: FoldoutGroup("Extra Information")]
        public string Name { get; private set; }

        [field: TextArea]
        [field: SerializeField]
        [field: FoldoutGroup("Extra Information")]
        public string Description { get; private set; }

        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public float ResearchTime { get; private set; }
        [field: SerializeField] public List<ResourceCount> Cost { get; private set; }
        [field: SerializeField] public ResearchCommand Command { get; private set; }
    }
}