using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects
{
    public interface ITileCreationEffectsProvider
    {
        void PlayParticle(string key, Vector3 position);
    }
}