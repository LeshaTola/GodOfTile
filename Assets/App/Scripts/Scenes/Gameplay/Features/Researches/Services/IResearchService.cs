using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Services
{
    public interface IResearchService
    {
        event Action<float> OnTimerChanged;
        
        IReadOnlyCollection<ResearchSystem> ResearchSystems { get; }
        RuntimeResearch ActiveResearch { get; }
        IReadOnlyCollection<RuntimeResearch> Researches { get; }
        float Timer { get; }

        void StartResearch(ResearchConfig research);
        void AddResearchSystem(ResearchSystem researchSystem);
        void RemoveResearchSystem(ResearchSystem researchSystem);
        event Action OnResearchSystemsCountChanged;
    }
}