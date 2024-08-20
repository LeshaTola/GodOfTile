using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Configs
{
    [CreateAssetMenu(fileName = "ResearchServiceConfig", menuName = "Configs/Research/Serivce")]
    public class ResearchServiceConfig:ScriptableObject
    {
        [field: SerializeField,FoldoutGroup("For Tests")] 
        public int StartLevel { get; private set;}
        [field: SerializeField,FoldoutGroup("For Tests")] 
        public List<ResearchConfig> OpenedResearches { get; private set;}

        [field: SerializeField] public List<RuntimeResearch> Researches { get; private set; }
    }
}