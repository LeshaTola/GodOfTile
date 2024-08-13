using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Factories
{
    public interface IChunksFactory
    {
        WorldChunk GetChunk(Chunk chunk);
    }
}