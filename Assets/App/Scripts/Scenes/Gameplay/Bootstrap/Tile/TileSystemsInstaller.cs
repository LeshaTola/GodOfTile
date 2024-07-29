using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class TileSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSystemFactory();
            BindSystemUIFactory();

            BindSystemService();
            BindResourceEarnerUIProvider();

            Container.BindInterfacesTo<ResourceEarnerService>().AsSingle();
        }

        private void BindResourceEarnerUIProvider()
        {
            Container.Bind<IResourceEarnerUIProvider>().To<ResourceEarnerUIProvider>().AsSingle();
        }

        private void BindSystemService()
        {
            Container
                .BindInterfacesTo<SystemsService>()
                .AsSingle();
        }

        private void BindSystemUIFactory()
        {
            Container
                .Bind<ISystemUIFactory>()
                .To<SystemUIFactory>()
                .AsSingle();
        }

        private void BindSystemFactory()
        {
            Container
                .Bind<ISystemsFactory>()
                .To<SystemsFactory>()
                .AsSingle();
        }
    }
}