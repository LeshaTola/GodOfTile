using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizators
{
    public class MapVisualizator : IMapVisualizator
    {
        private GameObject grid;
        private WorldChunk chunkTemplate;
        private Transform chunkContainer;
        private IChunksProvider chunksProvider;

        private List<WorldChunk> chunks = new();

        public MapVisualizator(GameObject grid, WorldChunk chunkTemplate, Transform chunkContainer,
            IChunksProvider chunksProvider)
        {
            this.grid = grid;
            this.chunkTemplate = chunkTemplate;
            this.chunkContainer = chunkContainer;
            this.chunksProvider = chunksProvider;
        }

        public void Show()
        {
            grid.SetActive(true);
            UpdateChunks();
        }

        public void Hide()
        {
            grid.SetActive(false);
            CleanupChunks();
        }

        private void UpdateChunks()
        {
            foreach (var chunk in chunksProvider.ClosedChunks)
            {
                var newChunk = Object.Instantiate(chunkTemplate, chunkContainer);
                newChunk.Initialize(chunk.Id);
                var newChunkTransform = newChunk.transform;
                newChunkTransform.localScale 
                    = new Vector3(chunk.Size.x, newChunkTransform.localScale.y, chunk.Size.y);
                newChunkTransform.localPosition 
                    = new Vector3(chunk.ChunkCenter.x, 0, chunk.ChunkCenter.y);
                chunks.Add(newChunk);
            }
        }

        private void CleanupChunks()
        {
            foreach (var chunk in chunks)
            {
                Object.Destroy(chunk.gameObject);
            }
            chunks.Clear();
        }
    }
}