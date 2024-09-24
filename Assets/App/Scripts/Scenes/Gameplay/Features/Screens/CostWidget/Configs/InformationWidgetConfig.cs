using App.Scripts.Scenes.Gameplay.Features.Popups.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Configs
{
    [CreateAssetMenu(
        fileName = "InformationWidgetConfig",
        menuName = "Configs/UI/InformationWidget"
    )]
    public class InformationWidgetConfig : ScriptableObject
    {
        [SerializeField]
        private TextColorConfig textColorConfig;

        [SerializeField]
        private string defaultText = "resources";

        [SerializeField]
        private string noCostText = "is free!";

        public TextColorConfig TextColorConfig => textColorConfig;

        public string DefaultText => defaultText;

        public string NoCostText => noCostText;
    }
}