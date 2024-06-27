using Assets.App.Scripts.Scenes.Gameplay.Features.Camera.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private GridConfig gridConfig;

		[SerializeField] private CinemachineVirtualCamera virtualCamera;
		[SerializeField] private CameraMovementConfig cameraMovementConfig;
		[SerializeField] private Transform cameraTarget;

		public override void InstallBindings()
		{
			BindVirtualCamera();
			BindGameInput();
			BindCameraController();

			BindGridProvider();
		}

		private void BindCameraController()
		{
			Container.BindInterfacesTo<CameraController>()
				.AsSingle()
				.WithArguments(cameraMovementConfig, cameraTarget);
		}

		private void BindVirtualCamera()
		{
			Container.Bind<CinemachineVirtualCamera>().FromInstance(virtualCamera);
		}

		private void BindGameInput()
		{
			Container.BindInterfacesTo<GameInput>().AsSingle();
		}

		private void BindGridProvider()
		{
			Container.Bind<IGridProvider>()
				.To<GridProvider>()
				.AsSingle()
				.WithArguments(gridConfig);
		}
	}
}
