using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Factories
{
    public interface IResourceUIFactory
    {
        IResourceUI GetRecourseUI(string resourceName);
    }
}
