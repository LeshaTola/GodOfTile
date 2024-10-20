using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views;
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