using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors
{
    public abstract class EffectorData : TileSystemData
    {
        [SerializeField] private IGetTilesStrategy getTilesStrategy;
        [SerializeField] private IValidationStrategy validationStrategy;

        public IGetTilesStrategy GetTilesStrategy => getTilesStrategy;

        public IValidationStrategy ValidationStrategy => validationStrategy;
    }

    public abstract class Effector : TileSystem
    {
        public Effector(Tile parentTile, IGridProvider gridProvider) : base(parentTile)
        {
        }

        public List<Vector2Int> GetAreaPositions()
        {
            var data = (EffectorData) Data;

            if (data == null)
            {
                return null;
            }

            return data.GetTilesStrategy.GetPositions(ParentTile.Position);
        }

        public List<Tile> GetValidTiles()
        {
            var data = (EffectorData) Data;

            if (data == null)
            {
                return null;
            }

            return data.ValidationStrategy.ValidateTiles(data.GetTilesStrategy.GetTiles(ParentTile.Position).ToList());
        }
    }
}