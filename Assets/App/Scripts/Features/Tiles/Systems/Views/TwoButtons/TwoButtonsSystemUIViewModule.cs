using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Features.Tiles.Systems.Views.TwoButtons
{
    public class TwoButtonsSystemUIViewModule
    {
        public string Text { get; }
        public LabeledCommand YesAction { get; }
        public LabeledCommand NoAction { get; }
        public ILocalizationSystem LocalizationSystem { get; }
        

        public TwoButtonsSystemUIViewModule(string text, LabeledCommand yesAction, LabeledCommand noAction,
            ILocalizationSystem localizationSystem)
        {
            Text = text;
            YesAction = yesAction;
            NoAction = noAction;
            LocalizationSystem = localizationSystem;
        }
    }
}