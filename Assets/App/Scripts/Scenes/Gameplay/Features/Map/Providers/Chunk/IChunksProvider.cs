using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk
{
    public interface IChunksProvider
    {
        HashSet<Items.Chunk> OpenedChunks { get; }
        HashSet<Items.Chunk> ClosedChunks { get; }
        void OpenChunk(Items.Chunk chunk);
        bool IsInOpenedChunk(Tile tile);
        bool IsInOpenedChunk(Vector2Int position);
        void OpenChunk(Vector2Int chunkId);
        bool IsOpenedNeighbour(Vector2Int id);
        event Action OnChunkOpened;
    }
}