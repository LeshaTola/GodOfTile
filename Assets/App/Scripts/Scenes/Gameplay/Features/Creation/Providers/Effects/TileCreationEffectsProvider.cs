using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Effects
{
    public class TileCreationEffectsProvider : ITileCreationEffectsProvider
    {
        private KeyPool<PooledParticle> keyPool;

        public TileCreationEffectsProvider(KeyPool<PooledParticle> keyPool)
        {
            this.keyPool = keyPool;
        }

        public void PlayParticle(string key, Vector3 position)
        {
            var particle = keyPool.Get(key);
            particle.transform.position = position;
            particle.Particle.Play();
        }
    }
}
