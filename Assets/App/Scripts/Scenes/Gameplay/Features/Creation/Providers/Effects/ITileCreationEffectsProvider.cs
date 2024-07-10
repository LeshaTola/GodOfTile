using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Effects
{
    public interface ITileCreationEffectsProvider
    {
        void PlayParticle(string key, Vector3 position);
    }
}
