using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Services
{
    public interface IResearchService
    {
        event Action<float> TimerChanged;
        event Action ResearchSystemsCountChanged;
        
        IReadOnlyCollection<ResearchSystem> ResearchSystems { get; }
        IReadOnlyCollection<RuntimeResearch> Researches { get; }
        
        RuntimeResearch ActiveResearch { get; }
        float Timer { get; }
        int Level { get; }

        void LevelUp();
        
        void StartResearch(ResearchConfig research);
        
        void AddResearchSystem(ResearchSystem researchSystem);
        void RemoveResearchSystem(ResearchSystem researchSystem);

        event Action<int> LevelChanged;
    }
}