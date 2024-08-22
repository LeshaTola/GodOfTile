using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Researches.Factories;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Services
{
    public class ResearchService : IUpdatable, IResearchService
    {
        public event Action OnResearchSystemsCountChanged;
        public event Action<float> OnTimerChanged;

        private ResearchServiceConfig config;
        private ITimeProvider timeProvider;
        private IResearchCommandsFactory researchCommandsFactory;

        private List<ResearchSystem> researchSystems = new();
        private List<RuntimeResearch> researches = new();

        public float Timer { get; private set; } = 0;
        public RuntimeResearch ActiveResearch { get; private set; }
        public IReadOnlyCollection<ResearchSystem> ResearchSystems => researchSystems;
        public IReadOnlyCollection<RuntimeResearch> Researches => researches;

        public ResearchService(ResearchServiceConfig config, ITimeProvider timeProvider,
            IResearchCommandsFactory researchCommandsFactory)
        {
            this.config = config;
            this.timeProvider = timeProvider;
            this.researchCommandsFactory = researchCommandsFactory;

            Initialize();
        }

        public void StartResearch(ResearchConfig research)
        {
            var runtimeResearch
                = researches.FirstOrDefault(x => x.ResearchConfig.Name.Equals(research.Name));
            if (runtimeResearch == null)
            {
                Debug.LogError($"Can't find research with such name {research.Name}");
                return;
            }

            Timer = runtimeResearch.ResearchConfig.ResearchTime;
            ActiveResearch = runtimeResearch;
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
            if (ActiveResearch == null)
            {
                return;
            }
            
            var speedMultiplier = researchSystems.Count;
            Timer -= timeProvider.DeltaTime * speedMultiplier;
            if (Timer <= 0)
            {
                researchCommandsFactory.GetResearch(ActiveResearch.ResearchConfig.Command).Execute();
                ActiveResearch.IsCompleate = true;
                ActiveResearch = null;
                Timer = 0;
            }
            OnTimerChanged?.Invoke(Timer);
        }

        private void Initialize()
        {
            researches = new List<RuntimeResearch>();
            foreach (var runtimeResearch in config.Researches)
            {
                if (runtimeResearch.IsCompleate)
                {
                    researchCommandsFactory.GetResearch(runtimeResearch.ResearchConfig.Command).Execute();
                }

                researches.Add(runtimeResearch);
            }
        }
    }
}