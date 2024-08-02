using App.Scripts.Modules.ObjectPool.KeyPools;
using App.Scripts.Modules.ObjectPool.KeyPools.Configs;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using App.Scripts.Scenes.Gameplay.Features.Grid;
using App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using App.Scripts.Scenes.Gameplay.Features.Grid.Visualizators;
using App.Scripts.Scenes.Gameplay.Features.Input;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Grid")]
        [SerializeField]
        private GridConfig gridConfig;

        [SerializeField]
        private GameObject grid;

        [Header("Camera")]
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [SerializeField]
        private CameraMovementConfig cameraMovementConfig;

        [SerializeField]
        private Transform cameraTarget;

        [Header("Particles")]
        [SerializeField]
        private ParticlesDatabase particlesDatabase;

        [SerializeField]
        private Transform particlesContainer;

        public override void InstallBindings()
        {
            CommandInstaller.Install(Container);
            RouterInstaller.Install(Container);

            BindTimeProviders();
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            BindGameInput();

            BindMainCamera();
            BindVirtualCamera();
            BindCameraController();

            BindParticlesKeyPool();

            BindGridProvider();
            BindGridVisualizator();
        }

        private void BindTimeProviders()
        {
            Container.Bind<ITimeProvider>().To<GameplayTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<ProjectTimeProvider>().AsSingle().WhenInjectedInto<CameraController>();
        }

        private void BindGridVisualizator()
        {
            Container
                .Bind<IGridVisualizator>()
                .To<GridVisualizator>()
                .AsSingle()
                .WithArguments(grid);
        }

        private void BindParticlesKeyPool()
        {
            Container
                .Bind<KeyPool<PooledParticle>>()
                .AsSingle()
                .WithArguments(particlesDatabase.Particles, particlesContainer);
        }

        private void BindCameraController()
        {
            Container
                .BindInterfacesTo<CameraController>()
                .AsSingle()
                .WithArguments(cameraMovementConfig, cameraTarget);
        }

        private void BindVirtualCamera()
        {
            Container.Bind<CinemachineVirtualCamera>().FromInstance(virtualCamera).AsSingle();
        }

        private void BindMainCamera()
        {
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        }

        private void BindGameInput()
        {
            Container.BindInterfacesTo<GameInput>().AsSingle();
        }

        private void BindGridProvider()
        {
            Container.Bind<IGridProvider>().To<GridProvider>().AsSingle().WithArguments(gridConfig);
        }
    }
}