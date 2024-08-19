using System;
using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Services
{
    public class ResearchService : IUpdatable, IResearchService
    {
        public event Action OnResearchSystemsCountChanged;
        
        private ResearchServiceConfig config;
        private ITimeProvider timeProvider;
        
        public float Timer = 0;
        private List<ResearchSystem> researchSystems = new();
        
        public ResearchConfig ActiveResearch { get; private set; }
        public IReadOnlyCollection<ResearchSystem> ResearchSystems => researchSystems;
        
        public ResearchService(ResearchServiceConfig config, ITimeProvider timeProvider)
        {
            this.config = config;
            this.timeProvider = timeProvider;
        }
        
        public void StartResearch(ResearchConfig research)
        {
            Timer = research.ResearchTime;
            ActiveResearch = research;
        }

        public void AddResearchSystem(ResearchSystem researchSystem)
        {
            researchSystems.Add(researchSystem);
            OnResearchSystemsCountChanged?.Invoke();
        }

        public void RemoveResearchSystem(ResearchSystem researchSystem)
        {
            researchSystems.Remove(researchSystem);
            OnResearchSystemsCountChanged?.Invoke();
        }
        
        public void Update()
        {
            var speedMultiplier = researchSystems.Count;
            Timer -= timeProvider.DeltaTime * speedMultiplier;
            if (Timer <= 0)
            {
                ActiveResearch.Command.Execute();
            }
        }
    }
}