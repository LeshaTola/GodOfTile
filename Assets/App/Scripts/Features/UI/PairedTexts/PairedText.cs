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
        private TMProLocalizer header;

        [SerializeField]
        private TMProLocalizer value;

        public TMProLocalizer Header => header;
        public TMProLocalizer Value => value;

        public void Init(ILocalizationSystem localizationSystem)
        {
            header.Init(localizationSystem);
            value.Init(localizationSystem);
        }

        public void Translate()
        {
            header.Translate();
            value.Translate();
        }
    }
}