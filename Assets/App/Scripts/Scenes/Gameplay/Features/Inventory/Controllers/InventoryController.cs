using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI;
using Features.StateMachineCore;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Controllers
{
	public class InventoryController : IInventoryController, ICleanupable
    {
        private IInventorySystem inventorySystem;
        private IInventoryUI inventoryUI;
        private IResourceUIFactory resourceUIFactory;

        public InventoryController(
            IInventorySystem inventorySystem,
            IInventoryUI inventoryUI,
            IResourceUIFactory resourceUIFactory
        )
        {
            this.inventorySystem = inventorySystem;
            this.inventoryUI = inventoryUI;
            this.resourceUIFactory = resourceUIFactory;

            inventorySystem.OnRecourseAmountChanged += OnRecourseAmountChanged;
            inventorySystem.InitializeResources();
        }

        private void OnRecourseAmountChanged(string resourceName, int amount)
        {
            if (!inventoryUI.HasRecourseUI(resourceName))
            {
                AddResourceUI(resourceName, amount);
                return;
            }
            inventoryUI.UpdateResource(resourceName, amount);
        }

        public void Cleanup()
        {
            inventorySystem.OnRecourseAmountChanged -= OnRecourseAmountChanged;
        }

        private void AddResourceUI(string resourceName, int amount)
        {
            var resourceUI = resourceUIFactory.GetRecourseUI(resourceName);
            resourceUI.UpdateAmount(amount);
            inventoryUI.AddRecourse(resourceName, resourceUI);
        }
    }
}
