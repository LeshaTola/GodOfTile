﻿using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Animations;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.Tiles;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Tile
{
    public class TilesInstaller : MonoInstaller
    {
        [SerializeField] private Features.Tiles.General.Tile tilePrefab;
        [SerializeField] private Transform tilesContainer;
        [SerializeField] private TileAnimationConfig tileAnimationConfig;
        [SerializeField] private TilesCreationConfig tilesCreationConfig;
        [SerializeField] private TilesDatabase tilesDatabase;
        [SerializeField] private CollectionConfig collectionConfig;

        public override void InstallBindings()
        {
            BindTilesFactory();

            BindCreationService();
            BindTileCreationEffectsProviders();
            BindRecipeProvider();
            BindTileUpdateService();

            BindTileAnimator();

            BindTileSelectionProvider();
            BindTileCollectionProvider();
            BindActiveTileProvider();
        }

        private void BindTileCreationEffectsProviders()
        {
            Container
                .Bind<ITileCreationEffectsProvider>()
                .To<TileCreationEffectsProvider>()
                .AsSingle();
        }

        private void BindTileUpdateService()
        {
            Container
                .Bind<ITilesUpdateService>()
                .To<TilesUpdateService>()
                .AsSingle()
                .WithArguments(tilesCreationConfig);
        }

        private void BindTileCollectionProvider()
        {
            Container
                .Bind<ITileCollectionProvider>()
                .To<TileCollectionProvider>()
                .AsSingle()
                .WithArguments(collectionConfig, tilesDatabase);
        }

        private void BindActiveTileProvider()
        {
            Container.Bind<IActiveTileProvider>()
                .To<ActiveTileProvider>()
                .AsSingle()
                .WithArguments(tilesDatabase);
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
                .WithArguments(tilesCreationConfig);
        }

        private void BindTilesFactory()
        {
            Container
                .Bind<ITilesFactory>()
                .To<TilesFactory>()
                .AsSingle()
                .WithArguments(tilePrefab, tilesContainer, tilesDatabase);
        }
    }
}