using App.Scripts.Scenes.Gameplay.Features.Researches.Commands;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Factories
{
    public class ResearchCommandsFactory : IResearchCommandsFactory
    {
        private DiContainer diContainer;
        
        public ResearchCommandsFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public ResearchCommand GetResearch(ResearchCommand original)
        {
            var type = original.GetType();
            var researchCommand = diContainer.Instantiate(type, new object[] {original.Data});
            return (ResearchCommand) researchCommand;
        }
    }
}