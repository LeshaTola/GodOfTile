using UnityEngine;
using UnityEngine.Audio;

namespace App.Scripts.Modules.Sounds.Services
{
    public class AudioService : IAudioService
    {
        private AudioMixer audioMixer;

        public AudioService(AudioMixer audioMixer)
        {
            this.audioMixer = audioMixer;
        }

        public void ChangeMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", ConvertVolume(volume));
        }

        public void ChangeMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", ConvertVolume(volume));
        }

        public void ChangeEffectsVolume(float volume)
        {
            audioMixer.SetFloat("EffectsVolume", ConvertVolume(volume));
        }

        private float ConvertVolume(float value)
        {
            value = Mathf.Clamp01(value);
            return Mathf.Lerp(-80f, 0f, value);
        }
    }
}