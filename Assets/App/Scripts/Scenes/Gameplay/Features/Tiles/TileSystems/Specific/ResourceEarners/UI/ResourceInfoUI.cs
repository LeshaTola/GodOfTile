using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI
{
    public class ResourceInfoUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMProLocalizer nameText;
        [SerializeField] private TMProLocalizer infoText;

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            nameText.Initialize(localizationSystem);
            infoText.Initialize(localizationSystem);
        }

        public void Setup(ResourceConfig resource, string info)
        {
            image.sprite = resource.Sprite;
            nameText.Key = resource.ResourceName;
            infoText.Key = info;
        }

        public void Translate()
        {
            nameText.Translate();
            infoText.Translate();
        }
    }
}