using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Factories;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarner.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Factories;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Factories.TileSystemUI;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class TileSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ISystemsFactory>()
                .To<SystemsFactory>()
                .AsSingle();
            Container
                .Bind<ISystemUIFactory>()
                .To<SystemUIFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<SystemsService>()
                .AsSingle();

            Container.Bind<IResourceEarnerTileSystemUIProvider>().To<ResourceEarnerTileSystemUIProvider>().AsSingle();
        }
    }
}