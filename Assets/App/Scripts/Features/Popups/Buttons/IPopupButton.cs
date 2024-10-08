﻿using System;
using App.Scripts.Modules.Localization;

namespace App.Scripts.Features.Popups.Buttons
{
    public interface IPopupButton
    {
        event Action onButtonClicked;

        void Initialize(ILocalizationSystem localizationSystem);
        void Translate();
        void UpdateText(string text);
    }
}