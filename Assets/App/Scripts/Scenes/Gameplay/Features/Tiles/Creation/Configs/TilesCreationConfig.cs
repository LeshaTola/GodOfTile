using System.Collections.Generic;
using App.Scripts.Modules.ObjectPool.KeyPools.Configs;
using App.Scripts.Modules.Sounds;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs
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
        
        [Header("Audio")]
        [SerializeField] private AudioDatabase audioDatabase;
        [field: ValueDropdown(nameof(GetKeys))]
        [field: SerializeField] public string CreateSoundKey { get; private set; }

        [field: ValueDropdown(nameof(GetKeys))]
        [field: SerializeField] public string UpdateSoundKey { get; private set; }
        
        [field: ValueDropdown(nameof(GetKeys))]
        [field: SerializeField] public string DestroySoundKey { get; private set; }
        
        public List<string> GetKeys()
        {
            if (audioDatabase == null)
            {
                return null;
            }
            return audioDatabase.GetKeys();
        }

        public string DestroyParticleKey => destroyParticleKey;
        public string UpdateParticleKey => updateParticleKey;

        public string CreationParticleKey => creationParticleKey;

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