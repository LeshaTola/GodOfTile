using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Configs
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

        public Color CorrectColor => correctColor;
        public Color WrongColor => wrongColor;

        public Color DefaultColor => defaultColor;
    }
}