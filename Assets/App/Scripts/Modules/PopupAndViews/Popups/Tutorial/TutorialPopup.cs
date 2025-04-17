using App.Scripts.Modules.Localization.Elements.Buttons;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupAndViews.General.Popup;
using Cysharp.Threading.Tasks;
using GifImporter;
using TMPro;
using UnityEngine;

namespace App.Scripts.Modules.PopupAndViews.Popups.Tutorial
{
    public class TutorialPopup:Popup
    {
        // [ValueDropdown(@"GetAudioKeys")] [SerializeField] private string _closeSound;
        
        [SerializeField] private TMPLocalizer _header;
        [SerializeField] private UnityEngine.UI.Image _tutorImage;
        [SerializeField] private GifPlayer _tutorGif;
        [SerializeField] private TMPLocalizer _info;
        
        [SerializeField] private TMPLocalizedButton _nextButton;
        [SerializeField] private TMPLocalizedButton _prevButton;
        
        [SerializeField] private TMPLocalizedButton _okButton;

        private TutorialPopupVM vm;

        private int tutorialIndex;
        
        public void Setup(TutorialPopupVM vm)
        {
            this.vm = vm;

            Initialize();
            LocalSetup();
            Translate();
        }
        
        public override async UniTask Hide()
        {
            await base.Hide();
            Cleanup();
        }

        private void Cleanup()
        {
            _header.Cleanup();
            _info.Cleanup();
            _okButton.Cleanup();
            _nextButton.Cleanup();
            _prevButton.Cleanup();
        }

        private void LocalSetup()
        {
            _header.Key = vm.Data.Header;
            
            UpdateTutorial(0);
            
            _okButton.UpdateText(vm.Data.Command.Label);
            _okButton.UpdateAction(() =>
            {
                // vm.SoundProvider.PlaySound(_closeSound);
                vm.Data.Command.Execute();
            });
            
            _prevButton.UpdateAction(() => ChangeIndex(-1));
            _nextButton.UpdateAction(() => ChangeIndex(1));
        }

        private void ChangeIndex(int i)
        {
            tutorialIndex = Mathf.Clamp(tutorialIndex + i, 0, vm.Data.Tutorials.Count - 1); 
            UpdateTutorial(tutorialIndex);
        }

        private void UpdateTutorial(int i)
        {
            _info.Key = vm.Data.Tutorials[i].Mesage;
            _info.Translate();

            if (vm.Data.Tutorials[i].Image != null)
            {
                _tutorGif.Gif = null;
                _tutorImage.sprite = vm.Data.Tutorials[i].Image;
            }
            else if(vm.Data.Tutorials[i].Gif != null)
            {
                _tutorImage.sprite = null;
                _tutorGif.Gif = vm.Data.Tutorials[i].Gif;
            }

            UpdateButtons(i);
        }

        private void UpdateButtons(int i)
        {
            _prevButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
            _okButton.gameObject.SetActive(false);
            
            if (i == 0)
            {
                _prevButton.gameObject.SetActive(false);
            }
            
            if (i == vm.Data.Tutorials.Count - 1)
            {
                _okButton.gameObject.SetActive(true);
                _nextButton.gameObject.SetActive(false);
            }
        }

        private void Initialize()
        {
            tutorialIndex = 0;
            
            _header.Initialize(vm.LocalizationSystem);
            _info.Initialize(vm.LocalizationSystem);
            _okButton.Initialize(vm.LocalizationSystem);
            _nextButton.Initialize(vm.LocalizationSystem);
            _prevButton.Initialize(vm.LocalizationSystem);
        }

        private void Translate()
        {
            _header.Translate();
            _okButton.Translate();
            _nextButton.Translate();
            _prevButton.Translate();
        }
    }
}