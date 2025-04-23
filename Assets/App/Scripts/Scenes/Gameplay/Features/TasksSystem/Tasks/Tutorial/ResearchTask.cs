using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.TasksSystem.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks.Tutorial
{
    public class ResearchTask : Task
    {
        [SerializeField] private ResearchServiceConfig config;
        [SerializeField ,ValueDropdown(@"GetResearches")] private string researchName;

        private readonly IResearchService researchService;

        public ResearchTask(IResearchService researchService)
        {
            this.researchService = researchService;
        }
        
        public override void Start()
        {
            researchService.OnResearchCompleted += OnResearchCompleted;
        }

        public override void Complete()
        {
            base.Complete();

            researchService.OnResearchCompleted -= OnResearchCompleted;
        }

        public override ProgressPair GetProgress()
        {
            return new ProgressPair()
            {
                Progress = 0,
                Target = 1
            };
        }

        public override void SetProgress(ProgressPair progress)
        {
        }

        public override void Import(Task original)
        {
            var concrete = (ResearchTask)original;
            researchName = concrete.researchName;
        }

        private void OnResearchCompleted(RuntimeResearch research)
        {
            if (research.ResearchConfig.Name.Equals(researchName))
            {
                Progress = 1;
            }
        }

        public List<string> GetResearches()
        {
            if (config == null)
            {
                return new List<string>();
            }
            
            return config.Researches.Select(x => x.ResearchConfig.Name).ToList();
        }
    }
}