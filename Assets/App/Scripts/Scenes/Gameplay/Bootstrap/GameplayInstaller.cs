using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.ObjectPool.KeyPools;
using App.Scripts.Modules.ObjectPool.KeyPools.Configs;
using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.Tasks.Configs;
using App.Scripts.Modules.Tasks.Factories;
using App.Scripts.Modules.Tasks.Providers;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using App.Scripts.Scenes.Gameplay.Features.Map.Factories.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Map.Items;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk.Cost;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;
using App.Scripts.Scenes.Gameplay.Features.Map.WaterMaterialController;
using App.Scripts.Scenes.Gameplay.Features.Map.WaterMaterialController.Configs;
using App.Scripts.Scenes.Gameplay.Features.Saves;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Tasks")]
        [SerializeField] private TaskProviderConfig taskProviderConfig;
        
        
        [Header("Grid")]
        [SerializeField] private GridConfig gridConfig;

        [SerializeField] private ChunkCostConfig chunkCostConfig;

        [SerializeField] private WorldGrid grid;
        [SerializeField] private WorldChunk chunkTemplate;
        [SerializeField] private Transform chunksContainer;

        [Header("Camera")]
        [SerializeField] private Camera mainCamera;

        [SerializeField] private CamerasDatabase camerasDatabase;

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private CameraMovementConfig cameraMovementConfig;
        [SerializeField] private Transform cameraTarget;

        [Header("Particles")]
        [SerializeField] private ParticlesDatabase particlesDatabase;

        [SerializeField] private Transform particlesContainer;
        [SerializeField] private WaterMaterialConfig waterMaterialConfig;


        public override void InstallBindings()
        {
            BindTaskService();
            
            
            CommandInstaller.Install(Container);
            RouterInstaller.Install(Container);

            BindCycleServices();

            Container.Bind<GameplaySavesController>().AsSingle();

            Container.Bind<IWaterMaterialController>().To<WaterMaterialController>().AsSingle()
                .WithArguments(waterMaterialConfig);

            BindGameInput();

            BindMainCamera();
            BindVirtualCamera();
            BindCameraController();

            BindParticlesKeyPool();

            Container.Bind<IChunksFactory>().To<ChunksFactory>().AsSingle();
            Container.Bind<ICameraSwitcher>().To<CameraSwitcher>().AsSingle().WithArguments(camerasDatabase);
            BindMapProviders();
            BindMapVisualizers();
        }

        private void BindTaskService()
        {
            Container.Bind<TasksContainerFactory>().AsSingle();
            Container.Bind<CompleteActionFactory>().AsSingle();
            Container.Bind<TaskFactory>().AsSingle();
            Container.Bind<TasksProvider>().AsSingle().WithArguments(taskProviderConfig);
        }

        private void BindCycleServices()
        {
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<IInitializeService>().To<InitializeService>().AsSingle();
            Container.Bind<ICleanupService>().To<CleanupService>().AsSingle();
        }

        private void BindMapVisualizers()
        {
            Container.Bind<IPool<WorldChunk>>().To<MonoBehObjectPool<WorldChunk>>().AsSingle()
                .WithArguments(chunkTemplate, chunksContainer, 10);
            Container.Bind<IPool<WorldGrid>>().To<MonoBehObjectPool<WorldGrid>>().AsSingle()
                .WithArguments(grid, chunksContainer, 10);

            Container
                .Bind<IChunkVisualizer>()
                .To<ChunkVisualizer>()
                .AsSingle();

            Container
                .Bind<IVisualizer>()
                .To<GridVisualizer>()
                .AsSingle();
        }

        private void BindParticlesKeyPool()
        {
            Container
                .Bind<KeyPool<PoolableParticle>>()
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

        private void BindMapProviders()
        {
            Container.Bind<IGridProvider>().To<GridProvider>().AsSingle().WithArguments(gridConfig);
            Container.Bind<IChunksProvider>().To<ChunksProvider>().AsSingle().WithArguments(gridConfig);
            Container.Bind<IChunkCostProvider>().To<ChunkCostProvider>().AsSingle().WithArguments(chunkCostConfig);
        }
    }
}