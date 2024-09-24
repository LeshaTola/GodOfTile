using System;
using Sirenix.OdinInspector;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Configs
{
    [Serializable]
    public class RuntimeResearch
    {
        [HorizontalGroup("")]
        public ResearchConfig ResearchConfig;

        [HorizontalGroup("")]
        public bool IsCompleate;
    }
}