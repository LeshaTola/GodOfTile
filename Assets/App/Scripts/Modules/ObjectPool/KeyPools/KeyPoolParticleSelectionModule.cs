using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Module.ObjectPool.KeyPools
{
    [Serializable]
    public class KeyPoolParticleSelectionModule
    {
        [SerializeField]
        private ParticlesDatabase particlesDatabase;

        [SerializeField]
        [ShowIf("@particlesDatabase != null")]
        [ValueDropdown(nameof(GetParticleKeys))]
        private List<string> particleKeys;

        public List<string> ParticleKeys
        {
            get => particleKeys;
        }

        public IEnumerable<string> GetParticleKeys()
        {
            if (particlesDatabase == null)
            {
                return null;
            }

            return new List<string>(particlesDatabase.Particles.Keys);
        }
    }
}
