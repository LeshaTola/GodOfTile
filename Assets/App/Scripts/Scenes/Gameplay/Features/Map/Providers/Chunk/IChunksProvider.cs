using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers
{
    public interface IChunksProvider
    {
        HashSet<Chunk> OpenedChunks { get; }
        HashSet<Chunk> ClosedChunks { get; }
        void OpenChunk(Chunk chunk);
        bool IsInOpenedChunk(Tile tile);
        bool IsInOpenedChunk(Vector2Int position);
        void OpenChunk(Vector2Int chunkId);
        bool IsOpenedNeighbour(Vector2Int id);
    }
}