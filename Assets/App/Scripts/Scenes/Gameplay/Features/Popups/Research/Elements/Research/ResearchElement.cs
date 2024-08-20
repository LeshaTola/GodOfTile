using System;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research
{
    public class ResearchElement:MonoBehaviour
    {
        public event Action OnResearchButtonClicked;
        
        [SerializeField] private Image image;
        [SerializeField] private Button researchButton;
        
        public void Setup(ResearchConfig researchConfig)
        {
            image.sprite = researchConfig.Sprite;
            
            researchButton.onClick.RemoveAllListeners();
            researchButton.onClick.AddListener(()=>OnResearchButtonClicked?.Invoke());
        }
    }
}