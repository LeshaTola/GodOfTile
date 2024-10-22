using App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Routers;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.ViewModels;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class RouterInstaller : Installer<RouterInstaller>
    {
        public override void InstallBindings()
        {
            BindInformationWidgetViewModule();
            
            BindBuyAreaPopupRouter();
            Container.Bind<ResearchPopupRouter>().AsSingle();
        }

        private void BindBuyAreaPopupRouter()
        {
            Container.Bind<IBuyAreaPopupRouter>().To<BuyAreaPopupRouter>().AsSingle();
        }

        private void BindInformationWidgetViewModule()
        {
            Container.Bind<IInformationWidgetViewModule>().To<InformationWidgetViewModule>().AsSingle();
        }
    }
}