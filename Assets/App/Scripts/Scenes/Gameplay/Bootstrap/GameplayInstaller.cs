using App.Scripts.Modules.ObjectPool.KeyPools;
using App.Scripts.Modules.ObjectPool.KeyPools.Configs;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using App.Scripts.Scenes.Gameplay.Features.Grid;
using App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using App.Scripts.Scenes.Gameplay.Features.Grid.Visualizators;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Materials.WaterMaterial;
using App.Scripts.Scenes.Gameplay.Features.Materials.WaterMaterial.Configs;
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

        [SerializeField] private Transform particlesContainer;
        [SerializeField] private WaterMaterialConfig waterMaterialConfig;

        public override void InstallBindings()
        {
            CommandInstaller.Install(Container);
            RouterInstaller.Install(Container);

            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<IInitializeService>().To<InitializeService>().AsSingle();
            Container.Bind<ICleanupService>().To<CleanupService>().AsSingle();

            Container.Bind<IWaterMaterialController>().To<WaterMaterialController>().AsSingle()
                .WithArguments(waterMaterialConfig);

            BindGameInput();

            BindMainCamera();
            BindVirtualCamera();
            BindCameraController();

            BindParticlesKeyPool();

            BindGridProvider();
            BindGridVisualizator();
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