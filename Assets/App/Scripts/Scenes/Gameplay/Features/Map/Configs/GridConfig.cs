using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Configs
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Configs/Map/Grid")]
    public class GridConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2Int ChunkSize { get; private set; }
        [field: SerializeField] public Vector2Int StartChunk { get; private set; }
        [field: SerializeField] public Vector2Int ChunksCount { get; private set; }
    }
}