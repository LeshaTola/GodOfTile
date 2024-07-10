using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Effects;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Update
{
    public class TilesUpdateService : ITilesUpdateService
    {
        private IGridProvider gridProvider;
        private IRecipeProvider recipeProvider;
        private ITileCreationEffectsService effectsService;
        private TilesCreationConfig config;

        public TilesUpdateService(
            IGridProvider gridProvider,
            IRecipeProvider recipeProvider,
            ITileCreationEffectsService effectsService,
            TilesCreationConfig config
        )
        {
            this.gridProvider = gridProvider;
            this.recipeProvider = recipeProvider;
            this.effectsService = effectsService;
            this.config = config;
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
                        new TileToUpdate() { Position = position, NewConfig = result }
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
            var oldTile = gridProvider.Grid[position.x, position.y];
            effectsService.PlayParticle(config.UpdateParticleKey, oldTile.transform.position);
            oldTile.Initialize(newTileConfig);
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
