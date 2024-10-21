using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.ObjectPool.KeyPools;
using App.Scripts.Modules.ObjectPool.KeyPools.Configs;
using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Modules.Sounds;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Bootstrap;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Configs;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation.Presenters;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.MainMenu.Bootstrap
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GridConfig gridConfig;
        [SerializeField] private TileInformationView tileInformationView;
        
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CamerasDatabase camerasDatabase;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [Header("Particles")]
        [SerializeField] private ParticlesDatabase particlesDatabase;
        [SerializeField] private Transform particlesContainer;

        [Header("Audio")]
        [SerializeField] private SoundProvider soundProvider;


        public override void InstallBindings()
        {
            CommandInstaller.Install(Container);
            MenuTileSystemUIProviders.Install(Container);
            
            
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<IInitializeService>().To<InitializeService>().AsSingle();
            Container.Bind<ICleanupService>().To<CleanupService>().AsSingle();

            BindGameInput();
            
            BindMainCamera();
            BindVirtualCamera();
            Container.Bind<ICameraSwitcher>().To<CameraSwitcher>().AsSingle().WithArguments(camerasDatabase);

            Container.Bind<IGridProvider>().To<GridProvider>().AsSingle().WithArguments(gridConfig);
            Container.Bind<IChunksProvider>().To<ChunksProvider>().AsSingle().WithArguments(gridConfig);
            
            
            Container.Bind<TileInformationPresenter>().AsSingle();
            Container.Bind<TileInformationView>().FromInstance(tileInformationView);

            BindParticlesKeyPool();
            Container.Bind<ISoundProvider>().FromInstance(soundProvider).AsSingle();
        }
        

        private void BindParticlesKeyPool()
        {
            Container
                .Bind<KeyPool<PoolableParticle>>()
                .AsSingle()
                .WithArguments(particlesDatabase.Particles, particlesContainer);
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
    }
}