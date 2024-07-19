using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using App.Scripts.Scenes.Gameplay.Features.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Factories;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Services;
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

        public TilesUpdateService(
            IGridProvider gridProvider,
            IRecipeProvider recipeProvider,
            ITileCreationEffectsProvider effectsService,
            TilesCreationConfig config,
            ISystemsService systemsService,
            ISystemsFactory systemsFactory
        )
        {
            this.gridProvider = gridProvider;
            this.recipeProvider = recipeProvider;
            this.effectsService = effectsService;
            this.config = config;
            this.systemsService = systemsService;
            this.systemsFactory = systemsFactory;
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
                    tilesForUpdate.Add(
                        new TileToUpdate() {Position = position, NewConfig = result}
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

            systemsService.StopSystems(tile.Config);

            var newSystems = systemsFactory.GetSystems(newTileConfig.Systems);
            newTileConfig.ActiveSystems = newSystems;
            tile.Initialize(newTileConfig);

            systemsService.StopSystems(tile.Config);
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