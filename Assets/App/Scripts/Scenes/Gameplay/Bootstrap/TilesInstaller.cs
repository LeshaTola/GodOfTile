using Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
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

        public override void InstallBindings()
        {
            BindTilesFactory();

            BindCreationService();
            Container.Bind<IRecipeProvider>().To<RecipeProvider>().AsSingle();
        }

        private void BindCreationService()
        {
            Container
                .BindInterfacesTo<TilesCreationService>()
                .AsSingle()
                .WithArguments(tileConfigId);
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
