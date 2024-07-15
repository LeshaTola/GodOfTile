namespace App.Scripts.Modules.Localization.Localizers
{
    public interface ITextLocalizer
    {
        public void Init(ILocalizationSystem localizationSystem);
        public void Translate();
    }
}