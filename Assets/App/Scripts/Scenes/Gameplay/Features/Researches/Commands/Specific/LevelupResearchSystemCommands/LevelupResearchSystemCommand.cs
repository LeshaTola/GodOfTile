using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Commands.Specific.LevelupResearchSystemCommands
{
    public class LevelupResearchSystemCommand: ResearchCommand
    {
        [SerializeField] private ResearchCommandData data;

        private IResearchService researchService;

        public override ResearchCommandData Data => data;

        public LevelupResearchSystemCommand(ResearchCommandData data, IResearchService researchService)
        {
            this.data = data;
            this.researchService = researchService;
        }
        
        public override void Execute()
        {
            researchService.LevelUp();
        }
    }
}