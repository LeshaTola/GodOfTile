using System.Collections.Generic;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Information
{
    public class InformationPopup : Popup
    {
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

        private IInformationViewModule viewModule;

        public void Setup(IInformationViewModule viewModule)
        {
            Cleanup();
            Initialize(viewModule);
            SetupUI(viewModule.TileConfig);
        }

        public void SetupUI(TileConfig tileConfig)
        {
            tileImage.sprite = tileConfig.TileSprite;
            tileName.Key = tileConfig.Name;
            description.Key = tileConfig.Description;

            SetupSystems(tileConfig.ActiveSystems);

            Translate();
        }

        public void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            closeButton.onClick.RemoveAllListeners();
            CleanUpSystems();
        }

        private void Initialize(IInformationViewModule viewModule)
        {
            this.viewModule = viewModule;

            header.Initialize(viewModule.LocalizationSystem);
            descriptionHeader.Initialize(viewModule.LocalizationSystem);
            
            tileName.Initialize(viewModule.LocalizationSystem);
            description.Initialize(viewModule.LocalizationSystem);
            
            closeButton.onClick.AddListener(viewModule.CloseCommand.Execute);
        }

        private void Translate()
        {
            header.Translate();
            descriptionHeader.Translate();
            
            tileName.Translate();
            description.Translate();
        }

        private void SetupSystems(List<TileSystem> tileSystems)
        {
            foreach (var system in tileSystems)
            {
                var uiProvider =
                    viewModule.TileSystemUIProvidersFactory.GetSystemUIProvider(system.Data.SystemUIProvider);
                var systemUI = uiProvider.GetSystemUI(system);
                Transform systemUITransform= systemUI.transform;
                systemUITransform.SetParent(tileSystemsContainer);
                systemUITransform.localPosition = Vector3.zero;
                systemUITransform.localScale = Vector3.one;

                systemUI.Setup();
            }
        }

        private void CleanUpSystems()
        {
            foreach (RectTransform child in tileSystemsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}