using App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.Routers;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class RouterInstaller : Installer<RouterInstaller>
    {
        public override void InstallBindings()
        {
            BindInformationPopupRouter();
            Container.Bind<IShopPopupRouter>().To<ShopPopupRouter>().AsSingle();
        }

        private void BindInformationPopupRouter()
        {
            Container.Bind<IInformationPopupRouter>().To<InformationPopupRouter>().AsSingle();
        }
    }
}