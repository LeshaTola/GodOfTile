using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup.ViewModules
{
    public interface IBuyAreaPopupViewModule
    {
        ILocalizationSystem LocalizationSystem { get; }
        IInformationWidgetViewModule WidgetViewModule { get; }
        ILabeledCommand BuyCommand { get; }
        ICommand CloseCommand { get; }
        List<ResourceCount> Resources { get; }
    }
}