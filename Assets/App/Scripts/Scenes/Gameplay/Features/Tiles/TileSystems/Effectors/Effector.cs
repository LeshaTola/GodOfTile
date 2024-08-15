using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.GetTilesStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using Sirenix.OdinInspector;
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
    
    public abstract class Effector: TileSystem
    {
        public Effector(Tile parentTile, IGridProvider gridProvider) : base(parentTile)
        {
        }
    }
}