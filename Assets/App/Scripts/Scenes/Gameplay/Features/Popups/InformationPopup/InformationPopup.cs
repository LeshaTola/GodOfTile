using App.Scripts.Features.Popups.Buttons;
using App.Scripts.Features.UI.PairedTexts;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup
{
    public class InformationPopup : Popup
    {
        [SerializeField]
        private Image tileImage;

        [SerializeField]
        private TMProLocalizer header;

        [SerializeField]
        [FoldoutGroup("Stats")]
        private TMProLocalizer statsHeader;

        [SerializeField]
        [FoldoutGroup("Stats")]
        private PairedText tileName;

        [SerializeField]
        [FoldoutGroup("Stats")]
        private PairedText type;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMProLocalizer descriptionHeader;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMProLocalizer description;

        [SerializeField]
        private PopupButton closeButton;

        private IInformationViewModule viewModule;

        public void Setup(IInformationViewModule viewModule)
        {
            Init(viewModule);
            UpdateUI(viewModule.TileConfig);
        }

        public void UpdateUI(TileConfig tileConfig)
        {
            tileImage.sprite = tileConfig.TileSprite;
            type.Value.Key = tileConfig.Type;
            tileName.Value.Key = tileConfig.Name;
            description.Key = tileConfig.Description;
            Translate();
        }

        public void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            closeButton.onButtonClicked -= viewModule.CloseCommand.Execute;
        }

        private void Init(IInformationViewModule viewModule)
        {
            this.viewModule = viewModule;

            header.Init(viewModule.LocalizationSystem);
            statsHeader.Init(viewModule.LocalizationSystem);
            descriptionHeader.Init(viewModule.LocalizationSystem);

            type.Init(viewModule.LocalizationSystem);
            tileName.Init(viewModule.LocalizationSystem);
            description.Init(viewModule.LocalizationSystem);

            closeButton.Init(viewModule.LocalizationSystem);
            closeButton.onButtonClicked += viewModule.CloseCommand.Execute;
        }

        private void Translate()
        {
            header.Translate();
            statsHeader.Translate();
            descriptionHeader.Translate();

            type.Translate();
            tileName.Translate();
            description.Translate();

            closeButton.Translate();
        }
    }
}