﻿using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class RouterInstaller : Installer<RouterInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInformationPopupRouter>().To<InformationPopupRouter>().AsSingle();
        }
    }
}