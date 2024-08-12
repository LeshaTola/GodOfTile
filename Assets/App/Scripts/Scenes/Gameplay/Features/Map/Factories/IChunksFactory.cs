using App.Scripts.Scenes.Gameplay.Features.Map.Providers;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizators;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Factories
{
    public interface IChunksFactory
    {
        WorldChunk GetChunk(Chunk chunk);
    }
}