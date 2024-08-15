using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.General
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileVisual visual;
        [SerializeField] private TileConfig config;

        [ReadOnly, InlineEditor, OdinSerialize]
        public TileConfig ActiveConfig;

        public TileVisual Visual => visual;
        public TileConfig Config => config;
        public Vector2Int Position { get; set; }

        public void Initialize(TileConfig config)
        {
            this.config = config;
            ActiveConfig = config;
            Visual.Initialize(config.Size, config.TypeMaterial, config.Building);
        }
    }
}