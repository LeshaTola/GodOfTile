using App.Scripts.Scenes.Gameplay.Features.Popups.Configs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost
{
    public class CostUI : MonoBehaviour
    {
        [SerializeField] private TextColorConfig colorConfig;
        
        [SerializeField] private Image resourceImage;
        [SerializeField] private TextMeshProUGUI costText;

        public void UpdateUI(Sprite sprite, int cost, bool isCorrect = true)
        {
            resourceImage.sprite = sprite;
            costText.text = cost.ToString();
            costText.color = isCorrect == true ? colorConfig.DefaultColor : colorConfig.WrongColor;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}