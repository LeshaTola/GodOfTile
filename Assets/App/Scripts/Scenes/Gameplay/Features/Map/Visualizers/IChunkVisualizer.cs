using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizers
{
    public interface IChunkVisualizer : IVisualizer
    {
        void UpdateChunks(Vector2Int vector2Int);
    }
}