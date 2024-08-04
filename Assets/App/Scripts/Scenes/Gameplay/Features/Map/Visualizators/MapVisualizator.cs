using System.Collections.Generic;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizators
{
    public class MapVisualizator : IMapVisualizator
    {
        private GameObject grid;
        private IPool<WorldChunk> chunksPool;
        private IChunksProvider chunksProvider;

        private List<WorldChunk> chunks = new();

        public MapVisualizator(GameObject grid,IPool<WorldChunk> chunksPool,
            IChunksProvider chunksProvider)
        {
            this.grid = grid;
            this.chunksPool = chunksPool;
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
                var worldChunk = chunksPool.Get();
                SetupWorldChunk(worldChunk, chunk);

                if (chunksProvider.IsOpenedNeighbour(chunk.Id))
                {
                    worldChunk.ShowUI();   
                }
                chunks.Add(worldChunk);
            }
        }

        private static void SetupWorldChunk(WorldChunk newChunk, Chunk chunk)
        {
            newChunk.Initialize(chunk.Id);
            var newChunkTransform = newChunk.transform;
            newChunkTransform.localScale
                = new Vector3(chunk.Size.x, newChunkTransform.localScale.y, chunk.Size.y);
            newChunkTransform.localPosition
                = new Vector3(chunk.ChunkCenter.x, 0, chunk.ChunkCenter.y);
        }

        private void CleanupChunks()
        {
            foreach (var worldChunk in chunks)
            {
                worldChunk.HideUI();
                chunksPool.Release(worldChunk);
            }
            chunks.Clear();
        }
    }
}