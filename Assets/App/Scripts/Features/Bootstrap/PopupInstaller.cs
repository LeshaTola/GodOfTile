using App.Scripts.Modules.PopupLogic.Animations.Animator;
using App.Scripts.Modules.PopupLogic.Configs;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Modules.PopupLogic.General.Providers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.Features.Bootstrap
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
            BindPopupProvider();
            BindPopupAnimator();
            BindPopupController();
        }

        private void BindPopupController()
        {
            Container
                .Bind<IPopupController>()
                .To<PopupController>()
                .AsSingle()
                .WithArguments(screenBlocker);
        }

        private void BindPopupAnimator()
        {
            Container.Bind<IPopupAnimator>().To<PopupAnimator>().AsSingle();
        }

        private void BindPopupProvider()
        {
            Container
                .Bind<IPopupProvider>()
                .To<PopupProvider>()
                .AsSingle()
                .WithArguments(popupDatabase, container);
        }
    }
}