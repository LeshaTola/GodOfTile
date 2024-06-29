using Assets.App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using Cinemachine;
using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private GridConfig gridConfig;

        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [SerializeField]
        private CameraMovementConfig cameraMovementConfig;

        [SerializeField]
        private Transform cameraTarget;

        [SerializeField]
        private ParticlesDatabase particlesDatabase;

        [SerializeField]
        private Transform particlesContainer;

        public override void InstallBindings()
        {
            BindGameInput();

            BindMainCamera();
            BindVirtualCamera();
            BindCameraController();

            Container
                .Bind<KeyPool<PooledParticle>>()
                .AsSingle()
                .WithArguments(particlesDatabase.Particles, particlesContainer);

            BindGridProvider();
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
