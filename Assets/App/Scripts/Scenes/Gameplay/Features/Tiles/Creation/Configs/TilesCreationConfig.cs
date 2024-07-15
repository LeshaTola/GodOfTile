using System.Collections.Generic;
using Module.ObjectPool.KeyPools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Configs
{
    [CreateAssetMenu(fileName = "TileCreationConfig", menuName = "Configs/Tiles/Creation")]
    public class TilesCreationConfig : ScriptableObject
    {
        [SerializeField]
        private ParticlesDatabase particlesDatabase;

        [SerializeField]
        [ShowIf("@particlesDatabase != null")]
        [ValueDropdown(nameof(GetParticleKeys))]
        private string creationParticleKey;

        [SerializeField]
        [ShowIf("@particlesDatabase != null")]
        [ValueDropdown(nameof(GetParticleKeys))]
        private string destroyParticleKey;

        [SerializeField]
        [ShowIf("@particlesDatabase != null")]
        [ValueDropdown(nameof(GetParticleKeys))]
        private string updateParticleKey;

        public string DestroyParticleKey
        {
            get => destroyParticleKey;
        }
        public string UpdateParticleKey
        {
            get => updateParticleKey;
        }
        public string CreationParticleKey
        {
            get => creationParticleKey;
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
