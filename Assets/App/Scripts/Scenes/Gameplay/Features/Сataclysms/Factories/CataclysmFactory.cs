using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers
{
    public class CataclysmFactory
    {
        private readonly DiContainer diContainer;

        public CataclysmFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public Cataclysm Get(CataclysmConfig config)
        {
            return diContainer.InstantiatePrefabForComponent<Cataclysm>(config.Prefab);
        }
    }
}