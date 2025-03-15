using System;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using UnityEngine;

namespace App.Scripts.Features.UI.PairedTexts
{
    [Serializable]
    public class PairedText : MonoBehaviour
    {
        [SerializeField]
        private TMPLocalizer header;

        [SerializeField]
        private TMPLocalizer value;

        public TMPLocalizer Header => header;
        public TMPLocalizer Value => value;

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            header.Initialize(localizationSystem);
            value.Initialize(localizationSystem);
        }

        public void Translate()
        {
            header.Translate();
            value.Translate();
        }

        public void Cleanup()
        {
            header.Cleanup();
            value.Cleanup();
        }
    }
}