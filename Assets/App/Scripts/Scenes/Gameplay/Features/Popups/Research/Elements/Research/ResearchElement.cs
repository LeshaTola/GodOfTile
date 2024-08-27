using System;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research
{
    public class ResearchElement : MonoBehaviour
    {
        public event Action OnResearchButtonClicked;

        [SerializeField] private Image image;
        [SerializeField] private Button researchButton;

        public void Setup(RuntimeResearch runtimeResearch)
        {
            image.sprite = runtimeResearch.ResearchConfig.Sprite;
            if (runtimeResearch.IsCompleate)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }

            researchButton.onClick.RemoveAllListeners();
            researchButton.onClick.AddListener(() => OnResearchButtonClicked?.Invoke());
        }
    }
}