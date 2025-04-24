using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Researches.Factories.Commands;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Services
{
    public class ResearchService : IUpdatable, IResearchService
    {
        public event Action<RuntimeResearch> OnResearchCompleted;
        public event Action OnResearchSystemsCountChanged;
        public event Action<float> OnTimerChanged;
        public event Action<int> OnLevelChanged;

        public ResearchServiceConfig Config { get; }
        private ITimeProvider timeProvider;
        private IResearchCommandsFactory researchCommandsFactory;

        private List<ResearchSystem> researchSystems = new();
        private List<RuntimeResearch> researches = new();

        public int Level { get; private set; }
        public float Timer { get; private set; } = 0;
        public RuntimeResearch ActiveResearch { get; private set; }
        public IReadOnlyCollection<ResearchSystem> ResearchSystems => researchSystems;
        public IReadOnlyCollection<RuntimeResearch> Researches => researches;

        public bool Active { get; set; } = true;

        public ResearchService(
            ResearchServiceConfig config,
            ITimeProvider timeProvider,
            IResearchCommandsFactory researchCommandsFactory)
        {
            Config = config;
            this.timeProvider = timeProvider;
            this.researchCommandsFactory = researchCommandsFactory;

            Initialize();
        }

        public void StartResearch(ResearchConfig research)
        {
            StartResearch(research.Name);
        }
        
        private void StartResearch(string researchName)
        {
            if (string.IsNullOrEmpty(researchName))
            {
                return;
            }
            
            var runtimeResearch = FindResearchByName(researchName);
            if (runtimeResearch == null)
            {
                Debug.LogError($"Can't find research with such name {researchName}");
                return;
            }

            StartResearch(runtimeResearch);
        }

        private void StartResearch(RuntimeResearch runtimeResearch)
        {
            Timer = runtimeResearch.ResearchConfig.ResearchTime;
            ActiveResearch = runtimeResearch;
        }

        public void LevelUp()
        {
            SetLevel(Level + 1);
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
            if (!Active || ActiveResearch == null)
            {
                return;
            }

            var speedMultiplier = Mathf.Clamp(researchSystems.Count, 0, Config.MaxResearchStation);
            
            Timer -= timeProvider.DeltaTime * speedMultiplier;
            if (Timer <= 0)
            {
                FinishResearch(ActiveResearch);
                ActiveResearch.IsCompleate = true;
                var researchBuffer = ActiveResearch;
                ActiveResearch = null;
                Timer = 0;

                OnResearchCompleted?.Invoke(researchBuffer);
            }

            OnTimerChanged?.Invoke(Timer);
        }

        public ResearchState GetState()
        {
            return new()
            {
                Level = Level,
                CompletedResearches = researches
                        .Where(x=>x.IsCompleate)
                        .Select(x=>x.ResearchConfig.Name)
                        .ToList(),
                ActiveResearch = ActiveResearch == null ? String.Empty : ActiveResearch.ResearchConfig.Name,
                ActiveResearchTimer = Timer
            };
        }

        public void SetState(ResearchState state)
        {
            Level = state.Level;
            foreach (var researchId in state.CompletedResearches)
            {
                var research = FindResearchByName(researchId);
                if (research == null)
                {
                    continue;
                }
                
                research.IsCompleate = true;
                FinishResearch(research);
            }
            StartResearch(state.ActiveResearch);
            Timer = state.ActiveResearchTimer;
        }

        private RuntimeResearch FindResearchByName(string researchId)
        {
            var research = researches.FirstOrDefault(x =>
                x.ResearchConfig.Name.Equals(researchId));
            return research;
        }

        private void FinishResearch(RuntimeResearch research)
        {
            researchCommandsFactory.GetResearch(research.ResearchConfig.Command).Execute();
        }

        private void Initialize()
        {
            SetLevel(Config.StartLevel);

            researches = new List<RuntimeResearch>();
            foreach (var runtimeResearch in Config.Researches)
            {
                researches.Add(new RuntimeResearch()
                {
                    ResearchConfig = runtimeResearch.ResearchConfig,
                    IsCompleate = runtimeResearch.IsCompleate
                });

                if (runtimeResearch.IsCompleate)
                {
                    researchCommandsFactory.GetResearch(runtimeResearch.ResearchConfig.Command).Execute();
                }
            }
        }

        private void SetLevel(int level)
        {
            Level = level;
            OnLevelChanged?.Invoke(Level);
        }
    }

    public class ResearchState
    {
        public int Level = 1;
        public List<string> CompletedResearches= new();
        public string ActiveResearch;
        public float ActiveResearchTimer;
    }
    
}