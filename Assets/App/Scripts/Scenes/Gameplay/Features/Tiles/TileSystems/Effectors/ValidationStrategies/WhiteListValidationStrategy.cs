using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public class WhiteListValidationStrategy : IValidationStrategy
    {
       [SerializeField] private List<TileSystem> systemsToAffect = new();

        public List<Tile> ValidateTiles(List<Tile> tiles)
        {
            return tiles.Where(tile =>
                tile.Config.ActiveSystems.Any(system =>
                    systemsToAffect.Any(affectSystem => affectSystem.GetType().Equals(system.GetType()) )
                )
            ).ToList();
        } 
        
        public List<TileSystem> GetValidSystems(List<Tile> tiles)
        {
            return tiles.SelectMany(tile =>
                tile.Config.ActiveSystems.Where(system =>
                    systemsToAffect.Any(affectSystem => affectSystem.GetType().Equals(system.GetType()) 
                    )
                )).ToList();;
        } 
    }
}