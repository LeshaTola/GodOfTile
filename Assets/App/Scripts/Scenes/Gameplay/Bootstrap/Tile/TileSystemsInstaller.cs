using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Tile
{
    public class TileSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSystemFactory();
            BindSystemUIFactory();

            BindSystemService();

            Container.BindInterfacesTo<ResourceEarnerService>().AsSingle();
            
            BindSpeedupResourceEarningEffectorUIProvider();
            BindResourceEarnerUIProvider();
        }

        private void BindSpeedupResourceEarningEffectorUIProvider()
        {
            Container.Bind<SpeedupResourceEarningEffectorUIProvider>().AsSingle();
        }

        private void BindResourceEarnerUIProvider()
        {
            Container.Bind<ResourceEarnerUIProvider>().AsSingle();
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