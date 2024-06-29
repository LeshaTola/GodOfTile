using Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Animations;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class TilesInstaller : MonoInstaller
    {
        [SerializeField]
        private string tileConfigId = "BetaTile"; //TODO Swap to SO ???

        [SerializeField]
        private Tile tilePrefab;

        [SerializeField]
        private Transform tilesContainer;

        [SerializeField]
        private TileAnimationConfig tileAnimationConfig;

        [SerializeField]
        private TilesCreationConfig tilesCreationConfig;

        public override void InstallBindings()
        {
            BindTilesFactory();

            BindCreationService();
            Container.Bind<IRecipeProvider>().To<RecipeProvider>().AsSingle();
            Container
                .Bind<ITileAnimator>()
                .To<TileAnimator>()
                .AsSingle()
                .WithArguments(tileAnimationConfig);
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
                .WithArguments(tilePrefab, tilesContainer);
        }
    }
}
