using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.Time.Services.TimeServices;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Time
{
    public class TimeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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