﻿using System;
using System.Collections.Generic;
using App.Scripts.Modules.Saves.Structs;
using App.Scripts.Modules.Sounds;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.Tiles;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation
{
    public class TilesCreationService : ITilesCreationService, ICleanupable
    {
        public event Action<Vector2Int, Tile> OnTilePlaced;

        private IGridProvider gridProvider;
        private ITilesFactory tileFactory;
        private IActiveTileProvider activeTileProvider;
        private ITileCreationEffectsProvider effectsService;
        private ITilesUpdateService updateService;
        private ISystemsService systemsService;
        private readonly ISoundProvider soundProvider;
        private IEffectorVisualProvider effectorVisualProvider;
        private TilesCreationConfig config;

        private Tile activeTile;

        public TilesCreationService(
            IGridProvider gridProvider,
            ITilesFactory tileFactory,
            IActiveTileProvider activeTileProvider,
            ITileCreationEffectsProvider effectsService,
            ITilesUpdateService updateService,
            ISystemsService systemsService,
            ISoundProvider soundProvider,
            IEffectorVisualProvider effectorVisualProvider,
            TilesCreationConfig config
        )
        {
            this.gridProvider = gridProvider;
            this.tileFactory = tileFactory;
            this.activeTileProvider = activeTileProvider;
            this.effectsService = effectsService;
            this.updateService = updateService;
            this.systemsService = systemsService;
            this.soundProvider = soundProvider;
            this.effectorVisualProvider = effectorVisualProvider;
            this.config = config;

            activeTileProvider.OnActiveTileChanged += OnActiveTileChanged;
        }

        public void StartPlacingTile()
        {
            if (activeTile != null || activeTileProvider.ActiveTileConfig == null)
            {
                return;
            }

            activeTile = tileFactory.GetTile(activeTileProvider.ActiveTileConfig);
        }

        public void StopPlacingTile()
        {
            if (activeTile == null)
            {
                return;
            }

            effectorVisualProvider.Cleanup();
            Object.Destroy(activeTile.gameObject);
            activeTile = null;
        }

        public void PlaceActiveTile()
        {
            if (activeTile == null || !gridProvider.IsValid(activeTile))
            {
                return;
            }

            var tileBuffer = activeTile;
            activeTile = null;
            systemsService.StartSystems(tileBuffer.Config);

            PlayCreationVFX(tileBuffer).Forget();
            OnTilePlaced?.Invoke(tileBuffer.Position, tileBuffer);
            
            for (var x = 0; x < tileBuffer.Config.Size.x; x++)
            {
                for (var y = 0; y < tileBuffer.Config.Size.y; y++)
                {
                    Vector2Int tileCoordinate =
                        new(tileBuffer.Position.x + x, tileBuffer.Position.y + y);
                    gridProvider.Grid[tileCoordinate.x, tileCoordinate.y] = tileBuffer;
                    updateService.UpdateConnectedTiles(tileCoordinate);
                }
            }
        }

        public void MoveActiveTile(Vector2Int gridPosition)
        {
            if (activeTile == null)
            {
                return;
            }

            activeTile.transform.position = new Vector3(
                gridPosition.x,
                activeTile.transform.position.y,
                gridPosition.y
            );
            activeTile.Position = gridPosition;

            ChangeState();
            effectorVisualProvider.Cleanup();
            effectorVisualProvider.Setup(activeTile);
        }

        public async UniTask RotateActiveTile()
        {
            if (activeTile == null)
            {
                return;
            }

            await activeTile.Visual.PlayRotation();
        }

        public void Cleanup()
        {
            activeTileProvider.OnActiveTileChanged -= OnActiveTileChanged;
        }

        private async UniTask PlayCreationVFX(Tile tile)
        {
            tile.Visual.SetState(TileState.Default);
            effectsService.PlayParticle(config.CreationParticleKey, tile.transform.position);
            soundProvider.PlaySound(config.CreateSoundKey);
            await tile.Visual.PlayCreation();
        }
        private async UniTask PlayDestroyVFX(Tile tile)
        {
            effectsService.PlayParticle(config.DestroyParticleKey, tile.transform.position);
            soundProvider.PlaySound(config.DestroySoundKey);
            await tile.Visual.PlayDestroying();
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

        private void OnActiveTileChanged()
        {
            StopPlacingTile();
            StartPlacingTile();
        }
     
        public MapState GetState()
        {
            Dictionary<JsonVector2Int, string> gridDictionary = new();

            foreach (var tile in gridProvider.Grid)
            {
                if (tile == null)
                {
                    continue;
                }
                
                JsonVector2Int jsonPos = new(tile.Position);
                if ( gridDictionary.ContainsKey(jsonPos))
                {
                    continue;
                }
                gridDictionary.Add(jsonPos, tile.Config.Id);
            }

            var grid = new List<KeyValuePair>();
            foreach (var item in gridDictionary)
            {
                grid.Add(new KeyValuePair()
                {
                    Position = item.Key,
                    Id = item.Value
                });
            }
            
            return new()
            {
                Grid = grid
            };
        }

        public void SetState(MapState state)
        {
            StartPlacingTile();
            
            foreach (var item in state.Grid)
            {
                activeTileProvider.SetActiveTileByID(item.Id);
                MoveActiveTile(new(item.Position.X,item.Position.Y));
                PlaceActiveTile();
            }
            
            StopPlacingTile();
        }

        public void PlaceTile(Vector2Int gridPosition, TileConfig tile)
        {
            PlayDestroyVFX(activeTile).Forget();
        }

        public async void DestroyTile(Vector2Int gridPosition)
        {
            var tile = gridProvider.Grid[gridPosition.x, gridPosition.y];
            await PlayDestroyVFX(tile);
            Object.Destroy(tile.gameObject);
            gridProvider.Grid[gridPosition.x, gridPosition.y] = null;
        }
    }

    public class MapState
    {
        public List<KeyValuePair> Grid;
    }

    public class KeyValuePair
    {
        public JsonVector2Int Position;
        public string Id;
    }
}