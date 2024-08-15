using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
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

        public override void InstallBindings()
        {
            Container.Bind<IPool<EffectorArea>>().To<MonoBehObjectPool<EffectorArea>>().AsSingle()
                .WithArguments(effectorAreaTemplate, 10, container);
            Container
                .Bind<IEffectorVisual>()
                .To<EffectorVisual>()
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