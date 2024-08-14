using App.Scripts.Scenes.Gameplay.Features.Map.Items;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Factories.Chunk
{
    public interface IChunksFactory
    {
        WorldChunk GetChunk(Items.Chunk chunk);
    }
}