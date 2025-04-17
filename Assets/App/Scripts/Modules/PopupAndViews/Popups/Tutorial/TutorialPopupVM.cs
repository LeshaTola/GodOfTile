using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Sounds.Providers;

namespace App.Scripts.Modules.PopupAndViews.Popups.Tutorial
{
    public class TutorialPopupVM
    {
        public ILocalizationSystem LocalizationSystem { get; }
        public TutorialPopupData Data { get; }
        public ISoundProvider SoundProvider { get; }

        public TutorialPopupVM(ILocalizationSystem localizationSystem,
            TutorialPopupData data,
            ISoundProvider soundProvider)
        {
            LocalizationSystem = localizationSystem;
            Data = data;
            SoundProvider = soundProvider;
        }
    }
}