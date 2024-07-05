using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using TileSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services
{
    public class TilesCreationService : ITilesCreationService
    {
        private IGridProvider gridProvider;
        private ITilesFactory tileFactory;
        private IRecipeProvider recipeProvider;
        private KeyPool<PooledParticle> keyPool;
        private TilesCreationConfig config;
        private string tileId;

        private Tile activeTile;
        private UniTask activeTask;

        public TilesCreationService(
            IGridProvider gridProvider,
            ITilesFactory tileFactory,
            string tileId,
            IRecipeProvider recipeProvider,
            KeyPool<PooledParticle> keyPool,
            TilesCreationConfig config
        )
        {
            this.gridProvider = gridProvider;
            this.tileFactory = tileFactory;
            this.tileId = tileId;
            this.recipeProvider = recipeProvider;
            this.keyPool = keyPool;
            this.config = config;
        }

        public void StartPlacingTile()
        {
            if (activeTile != null)
            {
                return;
            }

            activeTile = tileFactory.GetTile(tileId);
        }

        public void StopPlacingTile()
        {
            if (activeTile == null)
            {
                return;
            }

            GameObject.Destroy(activeTile.gameObject);
        }

        public void PlaceActiveTile()
        {
            if (activeTile == null || !gridProvider.IsValid(activeTile))
            {
                return;
            }

            PlayCreationVFX().Forget();
            for (int x = 0; x < activeTile.Config.Size.x; x++)
            {
                for (int y = 0; y < activeTile.Config.Size.y; y++)
                {
                    Vector2Int tileCoordinate =
                        new(activeTile.Position.x + x, activeTile.Position.y + y);
                    gridProvider.Grid[tileCoordinate.x, tileCoordinate.y] = activeTile;
                    UpdateConnectedTiles(tileCoordinate);
                }
            }

            activeTile = null;
        }

        public void FullFill()
        {
            for (int x = 0; x < gridProvider.GridSize.x; x++)
            {
                for (int y = 0; y < gridProvider.GridSize.y; y++)
                {
                    var tile = tileFactory.GetTile(tileId);
                    tile.transform.position = new Vector3(x, 0, y);
                }
            }
        }

        public void MoveActiveTile(Vector2Int gridPosition)
        {
            if (activeTile == null)
            {
                return;
            }

            activeTile.transform.position = new(
                gridPosition.x,
                activeTile.transform.position.y,
                gridPosition.y
            );
            activeTile.Position = gridPosition;

            ChangeState();
        }

        public async UniTask RotateActiveTile()
        {
            if (activeTile == null)
            {
                return;
            }

            await activeTile.Visual.PlayRotation();
        }

        private void UpdateConnectedTiles(Vector2Int tilePosition)
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

        private void Replace(TileConfig newTileConfig, Vector2Int position)
        {
            var oldTile = gridProvider.Grid[position.x, position.y];
            PlayParticle(config.UpdateParticleKey, oldTile.transform.position);
            oldTile.Initialize(newTileConfig);
        }

        private void ChangeState()
        {
            if (gridProvider.IsValid(activeTile))
            {
                activeTile.Visual.SetState(TileState.Correct);
            }
            else
            {
                activeTile.Visual.SetState(TileState.Wrong);
            }
        }

        private async UniTask PlayCreationVFX()
        {
            activeTile.Visual.SetState(TileState.Default);

            PlayParticle(config.CreationParticleKey, activeTile.transform.position);

            activeTask = activeTile.Visual.PlayCreation();
            await activeTask;
        }

        private void PlayParticle(string key, Vector3 position)
        {
            var particle = keyPool.Get(key);
            particle.transform.position = position;
            particle.Particle.Play();
        }
    }
}
