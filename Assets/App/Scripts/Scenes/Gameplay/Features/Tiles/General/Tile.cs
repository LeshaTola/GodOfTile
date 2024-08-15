using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General.Effectors;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.General
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileVisual visual;
        [SerializeField] private TileConfig config;

        public TileVisual Visual => visual;
        public TileConfig Config => config;
        public Vector2Int Position { get; set; }

        public void Initialize(TileConfig config)
        {
            this.config = config;
            Visual.Initialize(config.Size, config.TypeMaterial, config.Building);
        }
    }
}