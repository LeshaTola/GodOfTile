using System;
using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation
{
    public class TileInformationView:AnimatedView
    {
        public event Action OnCloseButtonClicked; 
        
        [SerializeField] private TMPLocalizer header;
        [SerializeField] private Image tileImage;
        [SerializeField] private TMPLocalizer tileName;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMPLocalizer descriptionHeader;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMPLocalizer description;

        [SerializeField] private Button closeButton;
        [SerializeField] private RectTransform tileSystemsContainer;
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            header.Initialize(localizationSystem);
            descriptionHeader.Initialize(localizationSystem);

            tileName.Initialize(localizationSystem);
            description.Initialize(localizationSystem);

            closeButton.onClick.AddListener(()=>OnCloseButtonClicked?.Invoke());
        }
        
        public void Setup(TileConfig tileConfig, List<SystemUI> systemUIs)
        {
            tileImage.sprite = tileConfig.TileSprite;
            tileName.Key = tileConfig.Name;
            description.Key = tileConfig.Description;

            SetupSystems(systemUIs);
            Translate();
        }

        public void Cleanup()
        {
            closeButton.onClick.RemoveAllListeners();
            CleanupSystems();
        }

        private void SetupSystems(List<SystemUI> systemUIs)
        {
            foreach (var systemUI in systemUIs)
            {
                if (systemUI == null)
                {
                    continue;
                }
                systemUI.transform.SetParent(tileSystemsContainer, false);
                systemUI.Setup();
            }
        }

        public void CleanupSystems()
        {
            foreach (RectTransform child in tileSystemsContainer)
            {
                Destroy(child.gameObject);
            }
        }
        
        private void Translate()
        {
            header.Translate();
            descriptionHeader.Translate();

            tileName.Translate();
            description.Translate();
        }
    }
}