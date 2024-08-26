using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.ViewModels
{
    public class ResearchSystemViewModel
    {
        public ResearchSystemViewModel(ILocalizationSystem localizationSystem, ResearchSystemData data)
        {
            LocalizationSystem = localizationSystem;
            Data = data;
        }

        public ILocalizationSystem LocalizationSystem { get; private set; }
        public ResearchSystemData Data { get; private set; } 
    }
}