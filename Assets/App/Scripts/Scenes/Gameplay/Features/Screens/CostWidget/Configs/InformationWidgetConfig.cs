using App.Scripts.Scenes.Gameplay.Features.Popups.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.Configs
{
    [CreateAssetMenu(
        fileName = "InformationWidgetConfig",
        menuName = "Configs/UI/InformationWidget"
    )]
    public class InformationWidgetConfig : ScriptableObject
    {
        [SerializeField] private TextColorConfig textColorConfig;
        [SerializeField] private string defaultText = "Resources";
        [SerializeField] private string noCostText = "Is free";
        
        public TextColorConfig TextColorConfig => textColorConfig;
        public string DefaultText => defaultText;
        public string NoCostText => noCostText;
    }
}