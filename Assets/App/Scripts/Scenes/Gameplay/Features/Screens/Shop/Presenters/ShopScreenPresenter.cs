using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.ShopViews.Presenter;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Presenters
{
    public class ShopScreenPresenter: IInitializable, ICleanupable
    {
        private ShopScreen shopScreen;
        private ShopViewPresenter shopViewPresenter;

        public ShopScreenPresenter(
            ShopScreen shopScreen, 
            ShopViewPresenter shopViewPresenter)
        {
            this.shopScreen = shopScreen;
            this.shopViewPresenter = shopViewPresenter;
        } 

        public void Initialize()
        {
            shopViewPresenter.Initialize();
        }

        public void Cleanup()
        {
            shopViewPresenter.Cleanup();
        }

        public void Setup()
        {
            shopViewPresenter.Setup();
        }

        public async UniTask Show()
        {
            await shopScreen.Show();
            
            await shopViewPresenter.Show();
        }

        public async UniTask Hide()
        {
            await shopViewPresenter.Hide();
            
            await shopScreen.Hide();
        }
    }
}