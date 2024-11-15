using App.Scripts.Modules.Localization;
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
        private readonly ILocalizationSystem localizationSystem;

        public ChunksFactory(IPool<WorldChunk> chunksPool, IBuyAreaPopupRouter buyAreaPopupRouter,
            ILocalizationSystem localizationSystem)
        {
            this.chunksPool = chunksPool;
            this.buyAreaPopupRouter = buyAreaPopupRouter;
            this.localizationSystem = localizationSystem;
        }

        public WorldChunk GetChunk(Items.Chunk chunk)
        {
            var worldChunk = chunksPool.Get();
            SetupWorldChunk(worldChunk, chunk);
            return worldChunk;
        }

        private void SetupWorldChunk(WorldChunk newChunk, Items.Chunk chunk)
        {
            newChunk.Initialize(() => { buyAreaPopupRouter.Show(chunk.Id); },localizationSystem);
            var newChunkTransform = newChunk.transform;
            newChunkTransform.localScale
                = new Vector3(chunk.Size.x, newChunkTransform.localScale.y, chunk.Size.y);
            newChunkTransform.localPosition
                = new Vector3(chunk.Center.x, 0, chunk.Center.y);
        }
    }
}