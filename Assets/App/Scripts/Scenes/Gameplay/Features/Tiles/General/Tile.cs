using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileVisual visual;
        [SerializeField] private TileConfig config;

        public TileVisual Visual => visual;
        public TileConfig Config => config;
        public Vector2Int Position { get; set; }
        public IEnumerable<ITileSystem> Systems { get; private set; }

        public void Initialize(TileConfig config, IEnumerable<ITileSystem> tileSystems)
        {
            this.config = config;
            Systems = tileSystems;
            Visual.Initialize(config.Size, config.TypeMaterial, config.Building);
        }
    }
}