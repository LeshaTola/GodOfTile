using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.Time.Configs;
using App.Scripts.Scenes.Gameplay.Features.Time.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Time.Services.TimeServices;
using App.Scripts.Scenes.Gameplay.Features.Time.UI;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Time
{
    public class TimeInstaller : MonoInstaller
    {
        [SerializeField] private TimeControllerUI timeControllerUI;
        [SerializeField] private TimeSpeedConfig timeSpeedConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeController>().AsSingle().WithArguments(timeSpeedConfig).NonLazy();
            Container.BindInterfacesAndSelfTo<TimeControllerUI>().FromInstance(timeControllerUI).AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();

            BindTimeProviders();
        }
        
        private void BindTimeProviders()
        {
            Container.Bind<ITimeProvider>().To<GameplayTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<ProjectTimeProvider>().AsSingle().WhenInjectedInto<CameraController>();
        }
    }
}