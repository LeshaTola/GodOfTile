using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public class WhiteListValidationStrategy : IValidationStrategy
    {
        [SerializeField]     
        [TypeFilter("GetFilteredTypes")]
        public List<Type> typesToAffect = new();

        public WhiteListValidationStrategy(List<Type> typesToAffect)
       {
           this.typesToAffect = typesToAffect;
       }

        public List<Tile> ValidateTiles(List<Tile> tiles)
        {
            return tiles.Where(tile =>
                tile.Config.ActiveSystems.Any(system =>
                    typesToAffect.Any(type => type.Equals(system.GetType()) )
                )
            ).ToList();
        } 
        
        public List<Tile> ValidateTiles(List<Tile> tiles, List<Type> whiteList)
        {
            return tiles.Where(tile =>
                tile.Config.ActiveSystems.Any(system =>
                    whiteList.Any(type => type.Equals(system.GetType()) )
                )
            ).ToList();
        } 
        
        public List<TileSystem> GetValidSystems(List<Tile> tiles)
        {
            return tiles.SelectMany(tile =>
                tile.Config.ActiveSystems.Where(system =>
                    typesToAffect.Any(type => type.Equals(system.GetType()) 
                    )
                )).ToList();;
        } 
        
        private IEnumerable<Type> GetFilteredTypes()
        {
            var baseType = typeof(TileSystem);
            return baseType.Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(baseType));
        }
    }
}