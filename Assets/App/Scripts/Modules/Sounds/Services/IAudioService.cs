namespace App.Scripts.Modules.Sounds.Services
{
    public interface IAudioService
    {
        public void ChangeMasterVolume(float volume);
        public void ChangeMusicVolume(float volume);
        public void ChangeEffectsVolume(float volume);
    }
}