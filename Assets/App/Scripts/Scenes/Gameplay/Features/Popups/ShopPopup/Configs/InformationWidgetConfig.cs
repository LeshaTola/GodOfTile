using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Configs
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

        public TextColorConfig TextColorConfig
        {
            get => textColorConfig;
        }
        public string DefaultText
        {
            get => defaultText;
        }
        public string NoCostText
        {
            get => noCostText;
        }
    }
}
