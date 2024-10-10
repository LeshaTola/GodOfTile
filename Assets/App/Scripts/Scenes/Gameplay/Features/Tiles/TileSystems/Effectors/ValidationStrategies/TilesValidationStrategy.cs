using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public class TilesValidationStrategy : IValidationStrategy
    {
        private List<TileConfig> whiteList;

        public TilesValidationStrategy(List<TileConfig> whiteList)
        {
            this.whiteList = whiteList;
        }

        public List<Tile> ValidateTiles(List<Tile> tiles)
        {
            return ValidateTiles(tiles, whiteList);
        }

        public List<TileSystem> ValidateSystems(List<Tile> tiles)
        {
            return ValidateTiles(tiles).SelectMany(tile => tile.Config.ActiveSystems).ToList();
        }

        public static List<Tile> ValidateTiles(List<Tile> tiles, List<TileConfig> whiteList)
        {
            return tiles.Where(tile =>
                whiteList.Any(x =>x.Id.Equals(tile.Config.Id))).ToList();
        }
    }
}