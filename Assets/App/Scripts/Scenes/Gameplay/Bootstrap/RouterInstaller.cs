using App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Routers;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class RouterInstaller : Installer<RouterInstaller>
    {
        public override void InstallBindings()
        {
            BindInformationWidgetViewModule();

            BindInformationPopupRouter();
            BindGameplayPopupRouter();
            BindBuyAreaPopupRouter();
            Container.Bind<ResearchPopupRouter>().AsSingle();
        }

        private void BindGameplayPopupRouter()
        {
            Container.Bind<IGameplayPopupRouter>().To<GameplayPopupRouter>().AsSingle();
        }

        private void BindBuyAreaPopupRouter()
        {
            Container.Bind<IBuyAreaPopupRouter>().To<BuyAreaPopupRouter>().AsSingle();
        }

        private void BindInformationWidgetViewModule()
        {
            Container.Bind<IInformationWidgetViewModule>().To<InformationWidgetViewModule>().AsSingle();
        }

        private void BindInformationPopupRouter()
        {
            Container.Bind<IInformationPopupRouter>().To<InformationPopupRouter>().AsSingle();
        }
    }
}