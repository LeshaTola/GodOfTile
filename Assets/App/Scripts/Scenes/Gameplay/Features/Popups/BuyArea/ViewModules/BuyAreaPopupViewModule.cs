using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.ViewModules
{
    public class BuyAreaPopupViewModule : IBuyAreaPopupViewModule
    {
        public BuyAreaPopupViewModule(List<ResourceCount> resources, ILocalizationSystem localizationSystem,
            IInformationWidgetViewModule widgetViewModule, ILabeledCommand buyCommand, ICommand closeCommand)
        {
            Resources = resources;
            LocalizationSystem = localizationSystem;
            WidgetViewModule = widgetViewModule;
            BuyCommand = buyCommand;
            CloseCommand = closeCommand;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public IInformationWidgetViewModule WidgetViewModule { get; }
        public ILabeledCommand BuyCommand { get; }
        public ICommand CloseCommand { get; }
        public List<ResourceCount> Resources { get; }
    }
}