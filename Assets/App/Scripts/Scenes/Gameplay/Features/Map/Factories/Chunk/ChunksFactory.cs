using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Scenes.Gameplay.Features.Map.Items;
using App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.Routers;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Factories.Chunk
{
    public class ChunksFactory : IChunksFactory
    {
        private IPool<WorldChunk> chunksPool;
        private IBuyAreaPopupRouter buyAreaPopupRouter;

        public ChunksFactory(IPool<WorldChunk> chunksPool, IBuyAreaPopupRouter buyAreaPopupRouter)
        {
            this.chunksPool = chunksPool;
            this.buyAreaPopupRouter = buyAreaPopupRouter;
        }

        public WorldChunk GetChunk(Items.Chunk chunk)
        {
            var worldChunk = chunksPool.Get();
            SetupWorldChunk(worldChunk, chunk);
            return worldChunk;
        }

        private void SetupWorldChunk(WorldChunk newChunk, Items.Chunk chunk)
        {
            newChunk.Initialize(() => { buyAreaPopupRouter.Show(chunk.Id); });
            var newChunkTransform = newChunk.transform;
            newChunkTransform.localScale
                = new Vector3(chunk.Size.x, newChunkTransform.localScale.y, chunk.Size.y);
            newChunkTransform.localPosition
                = new Vector3(chunk.Center.x, 0, chunk.Center.y);
        }
    }
}