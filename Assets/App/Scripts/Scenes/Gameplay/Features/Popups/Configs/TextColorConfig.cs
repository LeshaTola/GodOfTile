using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Configs
{
    [CreateAssetMenu(fileName = "TextColorConfig", menuName = "Configs/Text/Color")]
    public class TextColorConfig : ScriptableObject
    {
        [SerializeField]
        private Color correctColor = Color.green;

        [SerializeField]
        private Color wrongColor = Color.red;

        [SerializeField]
        private Color defaultColor = Color.white;

        public Color CorrectColor
        {
            get => correctColor;
        }
        public Color WrongColor
        {
            get => wrongColor;
        }
        public Color DefaultColor
        {
            get => defaultColor;
        }
    }
}
