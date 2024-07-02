using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileVisual visual;

        [SerializeField]
        private TileConfig config;

        public TileVisual Visual
        {
            get => visual;
        }
        public TileConfig Config
        {
            get => config;
        }
        public Vector2Int Position { get; set; }

        public void Initialize(TileConfig config)
        {
            this.config = config;
            Visual.Initialize(config.Size, config.TypeMaterial, config.Building);
        }
    }
}
