using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Factories.Chunk;
using App.Scripts.Scenes.Gameplay.Features.Map.Items;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizers
{
    public class ChunkVisualizer : IChunkVisualizer
    {
        private IChunksProvider chunksProvider;
        private IChunksFactory chunksFactory;

        private List<WorldChunk> chunks = new();

        public ChunkVisualizer(IChunksProvider chunksProvider, IChunksFactory chunksFactory)
        {
            this.chunksProvider = chunksProvider;
            this.chunksFactory = chunksFactory;
        }

        public void Hide()
        {
            CleanupChunks();
            chunksProvider.OnChunkOpened -= UpdateChunks;
        }

        public void Show()
        {
            SetupChunks();
            chunksProvider.OnChunkOpened += UpdateChunks;
        }

        public void UpdateChunks(Vector2Int vector2Int)
        {
            CleanupChunks();
            SetupChunks();
        }

        private void SetupChunks()
        {
            foreach (var chunk in chunksProvider.ClosedChunks)
            {
                var worldChunk = chunksFactory.GetChunk(chunk);

                if (chunksProvider.IsOpenedNeighbour(chunk.Id))
                {
                    worldChunk.ShowUI();
                }

                chunks.Add(worldChunk);
            }
        }

        private void CleanupChunks()
        {
            foreach (var worldChunk in chunks)
            {
                worldChunk.Release();
            }

            chunks.Clear();
        }
    }
}