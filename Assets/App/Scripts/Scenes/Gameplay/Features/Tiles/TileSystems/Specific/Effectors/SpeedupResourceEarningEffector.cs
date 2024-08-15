using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors
{
    public class SpeedupResourceEarningEffectorData : EffectorData
    {
        [SerializeField] private SpeedupResourceEarningEffectorUI tileUI;
        [SerializeField] private float earningAmountMultiplier;

        public override SystemUI TileUI => tileUI;

        public float EarningAmountMultiplier => earningAmountMultiplier;
    }

    public class SpeedupResourceEarningEffector : Effector
    {
        [SerializeField] private SpeedupResourceEarningEffectorData data;

        private readonly ITilesCreationService tilesCreationService;
        private List<TileSystem> boostedSystems = new();
        public override TileSystemData Data => data;

        public SpeedupResourceEarningEffector(
            Tile parentTile, IGridProvider gridProvider, ITilesCreationService tilesCreationService,
            SpeedupResourceEarningEffectorData data,
            SpeedupResourceEarningEffectorUIProvider speedupResourceEarningEffectorUIProvider) : base(parentTile,
            gridProvider)
        {
            this.tilesCreationService = tilesCreationService;
            this.data = data;
            data.GetTilesStrategy.Initialize(gridProvider);

            SystemUIProvider = speedupResourceEarningEffectorUIProvider;
        }

        public override void Start()
        {
            base.Start();
            tilesCreationService.OnTilePlaced += OnTilePlaced;
            BoostTiles();
        }


        public override void Stop()
        {
            tilesCreationService.OnTilePlaced -= OnTilePlaced;
            UnBoostTiles();
        }

        private void OnTilePlaced(Vector2Int position, Tile tile)
        {
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
                = data.ValidationStrategy.GetValidSystems(data.GetTilesStrategy.GetTiles(ParentTile.Position));
            foreach (var boostedSystem in boostedSystems)
            {
                ((ResourceEarnerSystemData) boostedSystem.Data).AmountPerSecond *= data.EarningAmountMultiplier;
            }
        }

        private void UnBoostTiles()
        {
            foreach (var boostedSystem in boostedSystems)
            {
                ((ResourceEarnerSystemData) boostedSystem.Data).AmountPerSecond /= data.EarningAmountMultiplier;
            }

            boostedSystems.Clear();
        }
    }
}