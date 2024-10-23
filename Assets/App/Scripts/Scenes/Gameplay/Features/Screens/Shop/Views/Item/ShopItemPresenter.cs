using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.Item
{
    public class ShopItemPresenter
    {
        private ShopItemView shopItemView;
        private CostWidgetPresenter costWidgetPresenter;
        private TileConfig tileConfig;
        private readonly ISoundProvider soundProvider;
        private IShopSystem shopSystem;

        public ShopItemPresenter(
            ShopItemView shopItemView,
            TileConfig tileConfig,
            ISoundProvider soundProvider,
            IShopSystem shopSystem)
        {
            this.shopItemView = shopItemView;
            this.tileConfig = tileConfig;
            this.soundProvider = soundProvider;
            this.shopSystem = shopSystem;
        }

        public void Initialize(CostWidgetPresenter costWidgetPresenter)
        {
            this.costWidgetPresenter = costWidgetPresenter;

            shopItemView.Initialize();
            shopItemView.OnBuyButtonClicked += OnBuyButtonClicked;
            shopItemView.OnPointerEntered += OnPointerEntered;
            shopItemView.OnPointerExited += OnPointerExited;
        }

        public void Cleanup()
        {
            shopItemView.Cleanup();
            shopItemView.OnBuyButtonClicked -= OnBuyButtonClicked;
            shopItemView.OnPointerEntered -= OnPointerEntered;
            shopItemView.OnPointerExited -= OnPointerExited;
        }

        public void Setup()
        {
            shopItemView.UpdateView(tileConfig.TileSprite);
        }

        public async UniTask Show()
        {
            await shopItemView.Show();
        }

        public async UniTask Hide()
        {
            await shopItemView.Hide();
        }

        private void OnPointerExited()
        {
            costWidgetPresenter.Hide();
        }

        private void OnPointerEntered()
        {
            costWidgetPresenter.Move(shopItemView.transform.position);
            costWidgetPresenter.UpdateView(tileConfig.Cost);
            costWidgetPresenter.Show();
        }

        private void OnBuyButtonClicked()
        {
            if (!shopSystem.TryBuyTile(tileConfig))
            {
                return;
            }
            
            soundProvider.PlaySound(shopItemView.ButtonSoundKey);
        }
    }
}