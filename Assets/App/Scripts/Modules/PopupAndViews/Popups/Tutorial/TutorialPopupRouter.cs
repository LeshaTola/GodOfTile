using System.Collections.Generic;
using App.Scripts.Features;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupAndViews.General.Controllers;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Modules.PopupAndViews.Popups.Tutorial
{
    public class TutorialPopupRouter
    {
        private readonly IPopupController popupController;
        private readonly ILocalizationSystem localizationSystem;
        private readonly ISoundProvider soundProvider;

        public TutorialPopupRouter(
            IPopupController popupController,
            ILocalizationSystem localizationSystem, ISoundProvider soundProvider)
        {
            this.popupController = popupController;
            this.localizationSystem = localizationSystem;
            this.soundProvider = soundProvider;
        }

        public async UniTask ShowPopup(TutorialPopupData popupData)
        {
            var popup = popupController.GetPopup<TutorialPopup>();

            SetupCommand(popupData);
            var viewModule = new TutorialPopupVM(localizationSystem, popupData, soundProvider);
            popup.Setup(viewModule);

            await popup.Show();
        }

        public async UniTask ShowPopup(string header, List<TutorialData> tutorials)
        {
            await ShowPopup(new TutorialPopupData()
            {
                Header = header,
                Tutorials = tutorials,
                Command = new CustomCommand(ConstStrings.CONFIRM, async () =>
                {
                    await popupController.HideLastPopup();
                })
            });
        }

        private void SetupCommand(TutorialPopupData popupData)
        {
            popupData.Command ??= new CustomCommand(ConstStrings.CONFIRM, async () => { await popupController.HideLastPopup(); });
        }
    }
}