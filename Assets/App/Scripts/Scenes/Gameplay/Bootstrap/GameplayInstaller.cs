using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid.Configs;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private GridConfig gridConfig;

		public override void InstallBindings()
		{
			BindGridProvider();
		}

		private void BindGridProvider()
		{
			Container.Bind<IGridProvider>()
				.To<GridProvider>()
				.WithArguments(gridConfig);
		}
	}
}
