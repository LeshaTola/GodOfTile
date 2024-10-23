using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public class SystemsValidationStrategy : IValidationStrategy
    {
        private List<Type> whiteList;

        public SystemsValidationStrategy(List<Type> whiteList)
        {
            this.whiteList = whiteList;
        }

        public List<Tile> ValidateTiles(List<Tile> tiles)
        {
            return ValidateTiles(tiles, whiteList);
        }

        public List<TileSystem> ValidateSystems(List<Tile> tiles)
        {
            return tiles.SelectMany(tile =>
                tile.Config.ActiveSystems.Where(system =>
                    whiteList.Any(type => type.Equals(system.GetType())
                    )
                )).ToList();
        }

        public static List<Tile> ValidateTiles(List<Tile> tiles, List<Type> whiteList)
        {
            return tiles.Where(tile =>
                tile.Config.ActiveSystems.Any(system =>
                    whiteList.Any(type => type.Equals(system.GetType()))
                )
            ).ToList();
        }

        public static IEnumerable<Type> GetFilteredTypes()
        {
            var baseType = typeof(TileSystem);
            return baseType.Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(baseType));
        }
    }
}