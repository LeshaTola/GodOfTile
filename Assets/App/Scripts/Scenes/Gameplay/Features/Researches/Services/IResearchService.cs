using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Services
{
    public interface IResearchService
    {
        bool Active { get; set; }

        event Action<float> OnTimerChanged;
        event Action OnResearchSystemsCountChanged;
        event Action<int> OnLevelChanged;
        event Action<RuntimeResearch> OnResearchCompleted;

        IReadOnlyCollection<ResearchSystem> ResearchSystems { get; }
        IReadOnlyCollection<RuntimeResearch> Researches { get; }

        RuntimeResearch ActiveResearch { get; }
        float Timer { get; }
        int Level { get; }

        void LevelUp();

        void StartResearch(ResearchConfig research);

        void AddResearchSystem(ResearchSystem researchSystem);
        void RemoveResearchSystem(ResearchSystem researchSystem);
    }
}