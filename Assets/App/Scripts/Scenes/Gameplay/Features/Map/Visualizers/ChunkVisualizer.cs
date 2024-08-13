using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Factories;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizers
{
    public class ChunkVisualizer: IChunkVisualizer
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
        }

        public void Show()
        {
            SetupChunks();
        }

        public void UpdateChunks()
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