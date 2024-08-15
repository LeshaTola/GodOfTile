using System.Collections.Generic;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Map.Items;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizers
{
    public class GridVisualizer : IVisualizer
    {
        private IPool<WorldGrid> gridPool;
        private IChunksProvider chunksProvider;

        private List<WorldGrid> grids = new();

        public GridVisualizer(IPool<WorldGrid> gridPool,IChunksProvider chunksProvider)
        {
            this.gridPool = gridPool;
            this.chunksProvider = chunksProvider;
        }

        public void Show()
        {
            foreach (var chunk in chunksProvider.OpenedChunks)
            {
                var grid = gridPool.Get();
                grid.transform.localPosition 
                    = new Vector3(chunk.Center.x, 0, chunk.Center.y);
                grids.Add(grid);
            }
        }

        public void Hide()
        {
            foreach (var grid in grids)
            {
                gridPool.Release(grid);
            }
        }
    }
}