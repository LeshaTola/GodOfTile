﻿using App.Scripts.Modules.ObjectPool.KeyPools;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects
{
    public class TileCreationEffectsProvider : ITileCreationEffectsProvider
    {
        private KeyPool<PoolableParticle> keyPool;

        public TileCreationEffectsProvider(KeyPool<PoolableParticle> keyPool)
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