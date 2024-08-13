using App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers;
using App.Scripts.Scenes.Gameplay.StateMachines.State.UI.Routers;
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
            BindShopPopupRouter();
        }

        private void BindGameplayPopupRouter()
        {
            Container.Bind<IGameplayPopupRouter>().To<GameplayPopupRouter>().AsSingle();
        }
        
        private void BindBuyAreaPopupRouter()
        {
            Container.Bind<IBuyAreaPopupRouter>().To<BuyAreaPopupRouter>().AsSingle();
        }

        private void BindShopPopupRouter()
        {
            Container.Bind<IShopPopupRouter>().To<ShopPopupRouter>().AsSingle();
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