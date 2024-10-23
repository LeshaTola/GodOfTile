using App.Scripts.Modules.Localization;

namespace App.Scripts.Features.Tiles.Systems.Views.OnlyText
{
    public class OnlyTextSystemSystemViewModel
    {
        public OnlyTextSystemSystemViewModel(ILocalizationSystem localizationSystem, string data)
        {
            LocalizationSystem = localizationSystem;
            Data = data;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public string Data { get; }
    }
}