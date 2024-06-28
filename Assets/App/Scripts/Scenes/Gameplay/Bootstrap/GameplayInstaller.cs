using Assets.App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Cinemachine;
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
        private Tile tilePrefab;

        [SerializeField]
        private Transform tilesContainer;

        [SerializeField]
        private Transform cameraTarget;

        public override void InstallBindings()
        {
            BindGameInput();

            BindMainCamera();
            BindVirtualCamera();
            BindCameraController();

            Container
                .Bind<ITilesFactory>()
                .To<TilesFactory>()
                .AsSingle()
                .WithArguments(tilePrefab, tilesContainer);

            Container.BindInterfacesTo<CreationService>().AsSingle().WithArguments("BetaTile");

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
