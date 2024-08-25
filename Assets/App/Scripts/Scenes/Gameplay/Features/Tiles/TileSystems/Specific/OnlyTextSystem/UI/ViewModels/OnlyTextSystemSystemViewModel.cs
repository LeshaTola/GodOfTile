using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.OnlyTextSystem.UI.ViewModels
{
    public class OnlyTextSystemSystemViewModel
    {
        public OnlyTextSystemSystemViewModel(ILocalizationSystem localizationSystem, string data)
        {
            LocalizationSystem = localizationSystem;
            Data = data;
        }

        public ILocalizationSystem LocalizationSystem { get; private set; }
        public string Data { get; private set; } 
    }
}