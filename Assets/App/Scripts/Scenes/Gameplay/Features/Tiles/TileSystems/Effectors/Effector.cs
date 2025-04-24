using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors
{
    public class EffectorData : TileSystemData
    {
        [SerializeField] private IGetTilesStrategy getTilesStrategy;
        [SerializeField] private IEffect effect;

        public IGetTilesStrategy GetTilesStrategy => getTilesStrategy;
        public IEffect Effect => effect;
        public override ISystemUIProvider SystemUIProvider => effect.SystemUIProvider;
    }

    public class Effector : TileSystem
    {
        [SerializeField] private EffectorData data;

        private readonly ITilesCreationService tilesCreationService;
        private readonly ITilesUpdateService tilesUpdateService;
        private List<TileSystem> boostedSystems = new();

        public override TileSystemData Data => data;

        public Effector(EffectorData data, Tile parentTile, IGridProvider gridProvider,
            ITilesCreationService tilesCreationService, ITilesUpdateService tilesUpdateService) : base(parentTile)
        {
            this.tilesCreationService = tilesCreationService;
            this.tilesUpdateService = tilesUpdateService;
            this.data = data;
            data.GetTilesStrategy.Initialize(gridProvider);
            data.Effect.Initialize(this);
        }

        public override void Start()
        {
            base.Start();
            tilesCreationService.OnTilePlaced += OnTilePlaced;
            tilesUpdateService.OnTileUpdated += OnTilePlaced;
            BoostTiles();
        }

        public override void Stop()
        {
            tilesCreationService.OnTilePlaced -= OnTilePlaced;
            tilesUpdateService.OnTileUpdated -= OnTilePlaced;
            UnBoostTiles();
        }

        public List<Vector2Int> GetAreaPositions()
        {
            return data.GetTilesStrategy.GetPositions(ParentTile.Position);
        }
        
        public List<Tile> GetValidTiles()
        {
            return data
                .Effect
                .ValidationStrategy
                .ValidateTiles(data.GetTilesStrategy.GetTiles(ParentTile.Position).ToList());
        }

        private void OnTilePlaced(Vector2Int position, Tile tile)
        {
            if (!data.GetTilesStrategy.IsValid(ParentTile.Position, position))
            {
                return;
            }
            
            UpdateBoostedTiles();
        }

        private void UpdateBoostedTiles()
        {
            UnBoostTiles();
            BoostTiles();
        }

        private void BoostTiles()
        {
            boostedSystems
                = data.Effect.ValidationStrategy.ValidateSystems(data.GetTilesStrategy.GetTiles(ParentTile.Position));
            foreach (var boostedSystem in boostedSystems)
            {
                boostedSystem.AddEffect(data.Effect);
            }
        }

        private void UnBoostTiles()
        {
            foreach (var boostedSystem in boostedSystems)
            {
                boostedSystem.RemoveEffect(data.Effect);
            }

            boostedSystems.Clear();
        }
    }
}