using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Tile
{
    public class GameplayTileSystemsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ResourceEarnerService>().AsSingle();
            Container.Bind<ChangeResourceEarningEffectorUIProvider>().AsSingle();
            Container.Bind<ResourceEarnerUIProvider>().AsSingle();
            Container.Bind<ResearchSystemUIProvider>().AsSingle();
        }
    }
}