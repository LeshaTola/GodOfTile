using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Factories.MonoFactories;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Level;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Researches.Factories;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Research
{
    public class ResearchInstaller:Zenject.MonoInstaller
    {
        [SerializeField] private LevelElement levelElement;
        [SerializeField] private ResearchElement researchElement;
        
        [Header("Research")]
        [SerializeField] private ResearchServiceConfig researchServiceConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IResearchCommandsFactory>().To<ResearchCommandsFactory>().AsSingle();
            Container.Bind<IFactory<LevelElement>>().To<MonoFactory<LevelElement>>()
                .AsSingle().WithArguments(levelElement);
            
            Container.Bind<IFactory<ResearchElement>>().To<MonoFactory<ResearchElement>>()
                .AsSingle().WithArguments(researchElement);
            
            Container.BindInterfacesTo<ResearchService>().AsSingle().WithArguments(researchServiceConfig);
        }
    }
}