using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs
{
    [CreateAssetMenu(fileName = "ChunkFilling", menuName = "Configs/Map/Chunk Filling")]
    public class ChunkFilling : ScriptableObject
    {
        [HorizontalGroup("ToggleButtons")] 
        [Button("Enable All Tiles", ButtonSizes.Medium)]
        private void EnableAllTiles()
        {
            foreach (var tile in Tiles)
            {
                tile.IsActive = true;
            }
        }

        [HorizontalGroup("ToggleButtons")] 
        [Button("Disable All Tiles", ButtonSizes.Medium)]
        private void DisableAllTiles()
        {
            foreach (var tile in Tiles)
            {
                tile.IsActive = false;
            }
        }
        
        [field: SerializeField] public Vector2Int ChunkId { get; private set; }
        [field: SerializeField] public List<TileWithPosition> Tiles { get; private set; } = new();
    }
}