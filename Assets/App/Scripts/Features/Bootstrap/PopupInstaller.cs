using Assets.App.Scripts.Features.Popups.InformationPopup.Animator;
using Module.PopupLogic.Configs;
using Module.PopupLogic.General.Controller;
using Module.PopupLogic.General.Providers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.App.Scripts.Bootstrap
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField]
        private Image screenBlocker;

        [SerializeField]
        private PopupDatabase popupDatabase;

        [SerializeField]
        private Transform container;

        public override void InstallBindings()
        {
            Container
                .Bind<IPopupProvider>()
                .To<PopupProvider>()
                .AsSingle()
                .WithArguments(popupDatabase, container);
            Container.Bind<IPopupAnimator>().To<PopupAnimator>().AsSingle();
            Container
                .Bind<IPopupController>()
                .To<PopupController>()
                .AsSingle()
                .WithArguments(screenBlocker);
        }
    }
}
