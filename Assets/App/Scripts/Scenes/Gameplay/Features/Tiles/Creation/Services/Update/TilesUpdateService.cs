using System.Collections.Generic;
using App.Scripts.Modules.Sounds;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update
{
    public class TilesUpdateService : ITilesUpdateService
    {
        private IGridProvider gridProvider;
        private IRecipeProvider recipeProvider;
        private ITileCreationEffectsProvider effectsService;
        private TilesCreationConfig config;
        private ISystemsService systemsService;
        private ISystemsFactory systemsFactory;
        private readonly ISoundProvider soundProvider;
        private readonly ITileCollectionProvider tileCollectionProvider;

        public TilesUpdateService(
            IGridProvider gridProvider,
            IRecipeProvider recipeProvider,
            ITileCreationEffectsProvider effectsService,
            TilesCreationConfig config,
            ISystemsService systemsService,
            ISystemsFactory systemsFactory,
            ISoundProvider soundProvider,
            ITileCollectionProvider tileCollectionProvider
        )
        {
            this.gridProvider = gridProvider;
            this.recipeProvider = recipeProvider;
            this.effectsService = effectsService;
            this.config = config;
            this.systemsService = systemsService;
            this.systemsFactory = systemsFactory;
            this.soundProvider = soundProvider;
            this.tileCollectionProvider = tileCollectionProvider;
        }

        public void UpdateConnectedTiles(Vector2Int tilePosition)
        {
            var neighbors = gridProvider.GetCoveringTiles(tilePosition);
            neighbors.Add(tilePosition);

            List<TileToUpdate> tilesForUpdate = new();
            foreach (var position in neighbors)
            {
                if (!gridProvider.IsValid(position))
                {
                    continue;
                }

                var result = UpdateTile(position);
                if (result != null)
                {

                    var cloneResult = Object.Instantiate(result);
                    tilesForUpdate.Add(
                        new TileToUpdate() {Position = position, NewConfig = cloneResult}
                    );
                }
            }

            foreach (var tile in tilesForUpdate)
            {
                Replace(tile.NewConfig, tile.Position);
            }
        }

        private void Replace(TileConfig newTileConfig, Vector2Int position)
        {
            var tile = gridProvider.Grid[position.x, position.y];
            effectsService.PlayParticle(config.UpdateParticleKey, tile.transform.position);
            soundProvider.PlaySound(config.UpdateSoundKey);

            systemsService.StopSystems(tile.Config);

            var newSystems = systemsFactory.GetSystems(newTileConfig.Systems, tile);
            newTileConfig.ActiveSystems = newSystems;
            tile.Initialize(newTileConfig);

            systemsService.StartSystems(tile.Config);
            tileCollectionProvider.AddIfNotContains(newTileConfig);
        }

        private TileConfig UpdateTile(Vector2Int tilePosition)
        {
            var neighbors = gridProvider.GetCoveringTiles(tilePosition);

            var allConfigs = new List<TileConfig>();
            foreach (var neighbor in neighbors)
            {
                var tile = gridProvider.Grid[neighbor.x, neighbor.y];
                if (tile != null)
                    allConfigs.Add(tile.Config);
            }

            return recipeProvider.GetRecipe(
                allConfigs,
                gridProvider.Grid[tilePosition.x, tilePosition.y].Config
            );
        }
    }
}