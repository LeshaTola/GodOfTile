using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.UI
{
    public class CataclysmViewPresenter : IInitializable, ICleanupable
    {
        private readonly CataclysmView view;
        private readonly CataclysmsProvider cataclysmsProvider;

        public CataclysmViewPresenter(CataclysmView view, CataclysmsProvider cataclysmsProvider)
        {
            this.view = view;
            this.cataclysmsProvider = cataclysmsProvider;
        }

        public void Initialize()
        {
            cataclysmsProvider.OnCataclysmChanged += UpdateCataclysm;
            cataclysmsProvider.OnTimerChanged += view.UpdateTimer;
        }

        public void Cleanup()
        {
            cataclysmsProvider.OnCataclysmChanged -= UpdateCataclysm;
            cataclysmsProvider.OnTimerChanged -= view.UpdateTimer;
        }

        private void UpdateCataclysm(CataclysmConfig config)
        {
            view.SetCataclysmImage(config.Sprite);
        }

        public void Show()
        {
            view.Show().Forget();
        }
    }
}