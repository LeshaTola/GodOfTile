using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.UIProvidersFactory;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General.Effectors;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Tile
{
    public class TileSystemsInstaller : MonoInstaller
    {
        [SerializeField] private EffectorArea effectorAreaTemplate;
        [SerializeField] private Transform container;
        [SerializeField] private SystemsUIsDatabase systemsUIsDatabase;
        public override void InstallBindings()
        {
            Container.Bind<IPool<EffectorArea>>().To<MonoBehObjectPool<EffectorArea>>().AsSingle()
                .WithArguments(effectorAreaTemplate, 10, container);
            Container
                .Bind<IEffectorVisualProvider>()
                .To<EffectorVisualProvider>()
                .AsSingle();
            Container
                .Bind<ITileSystemUIProvidersFactory>()
                .To<TileSystemUIProvidersFactory>()
                .AsSingle();
            
            BindSystemFactory();
            BindSystemUIFactory();


            BindSystemService();

            Container.BindInterfacesTo<ResourceEarnerService>().AsSingle();

            BindSpeedupResourceEarningEffectorUIProvider();
            BindResourceEarnerUIProvider();
        }

        private void BindSpeedupResourceEarningEffectorUIProvider()
        {
            Container.Bind<ChangeResourceEarningEffectorUIProvider>().AsSingle();
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
                .AsSingle()
                .WithArguments(systemsUIsDatabase);
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