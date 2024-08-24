using System.Security.Cryptography;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Level
{
    public class LevelElement:MonoBehaviour
    {
        [SerializeField] private TMProLocalizer header;
        [SerializeField] private RectTransform researchesContainer;

        public void Setup(ILocalizationSystem localizationSystem, string headerText)
        {
            header.Initialize(localizationSystem);
            header.Key = headerText;
            header.Translate();
        }
        
        public void AddResearch(ResearchElement researchElement)
        {
            researchElement.transform.SetParent(researchesContainer,false);
        }

        public void Cleanup()
        {
            foreach (Transform child in researchesContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}