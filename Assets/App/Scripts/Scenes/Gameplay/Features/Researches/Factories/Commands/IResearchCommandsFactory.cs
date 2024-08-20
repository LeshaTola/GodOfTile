using App.Scripts.Scenes.Gameplay.Features.Researches.Commands;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Factories
{
    public interface IResearchCommandsFactory
    {
        ResearchCommand GetResearch(ResearchCommand original);
    }
}