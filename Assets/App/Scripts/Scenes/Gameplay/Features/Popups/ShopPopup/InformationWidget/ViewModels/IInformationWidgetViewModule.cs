﻿using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information
{
    public interface IInformationWidgetViewModule
    {
        ICostUIFactory CostUIFactory { get; }
        ILocalizationSystem LocalizationSystem { get; }
        TileConfig TileConfig { get; }
    }
}
