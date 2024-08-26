﻿using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.ViewModels
{
    public interface IShopViewModule
    {
        IItemFactory ItemFactory { get; }
        ILocalizationSystem LocalizationSystem { get; }
        IShopSystem ShopSystem { get; }
        IInformationWidgetViewModule ItemInformationWidget { get; }
    }
}