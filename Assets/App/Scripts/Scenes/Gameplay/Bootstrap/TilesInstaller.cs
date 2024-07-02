using Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Animations;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
	public class TilesInstaller : MonoInstaller
	{
		[SerializeField]
		private Tile tilePrefab;

		[SerializeField]
		private Transform tilesContainer;

		[SerializeField]
		private TileAnimationConfig tileAnimationConfig;

		[SerializeField]
		private TilesCreationConfig tilesCreationConfig;

		[SerializeField]
		private TilesDatabase tilesDatabase;

		[SerializeField]
		[ValueDropdown(nameof(GetTilesIds))]
		private string tileConfigId;

		public override void InstallBindings()
		{
			BindTilesFactory();

			BindCreationService();
			BindRecipeProvider();

			BindTileAnimator();

			BindTileSelectionProvider();
		}

		private void BindTileSelectionProvider()
		{
			Container.Bind<ITileSelectionProvider>().To<TileSelectionProvider>().AsSingle();
		}

		private void BindTileAnimator()
		{
			Container
				.Bind<ITileAnimator>()
				.To<TileAnimator>()
				.AsSingle()
				.WithArguments(tileAnimationConfig);
		}

		private void BindRecipeProvider()
		{
			Container.Bind<IRecipeProvider>().To<RecipeProvider>().AsSingle();
		}

		private void BindCreationService()
		{
			Container
				.BindInterfacesTo<TilesCreationService>()
				.AsSingle()
				.WithArguments(tileConfigId, tilesCreationConfig);
		}

		private void BindTilesFactory()
		{
			Container
				.Bind<ITilesFactory>()
				.To<TilesFactory>()
				.AsSingle()
				.WithArguments(tilePrefab, tilesContainer, tilesDatabase);
		}

		private IEnumerable<string> GetTilesIds()
		{
			if (tilesDatabase == null)
			{
				return null;
			}

			return new List<string>(tilesDatabase.Configs.Keys);
		}
	}
}
