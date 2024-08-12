using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Map.Factories;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizators
{
    public class MapVisualizator : IMapVisualizator
    {
        private GameObject grid;
        private IChunksProvider chunksProvider;
        private IChunksFactory chunksFactory;
        
        private List<WorldChunk> chunks = new();

        public MapVisualizator(GameObject grid, IChunksProvider chunksProvider,IChunksFactory chunksFactory)
        {
            this.grid = grid;
            this.chunksProvider = chunksProvider;
            this.chunksFactory = chunksFactory;
        }

        public void UpdateChunks()
        {
            CleanupChunks();
            SetupChunks();
        }

        public void Show()
        {
            grid.SetActive(true);
            SetupChunks();
        }

        public void Hide()
        {
            grid.SetActive(false);
            CleanupChunks();
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