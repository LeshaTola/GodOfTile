using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Controllers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class InventoryInstaller : MonoInstaller
    {
        [Header("Inventory")]
        [SerializeField]
        private ResourcesDatabase resourcesDatabase;

        [SerializeField]
        private InventoryUI inventoryUI;

        [SerializeField]
        private ResourceUI resourceUI;

        [SerializeField]
        private RectTransform container;

        public override void InstallBindings()
        {
            BindResourceUIFactory();

            BindInventoryUI();
            BindInventorySystem();
            BindInventoryController();
        }

        private void BindInventoryController()
        {
            Container.Bind<IInventoryController>().To<InventoryController>().AsSingle().NonLazy();
        }

        private void BindInventorySystem()
        {
            Container
                .Bind<IInventorySystem>()
                .To<InventorySystem>()
                .AsSingle()
                .WithArguments(resourcesDatabase);
        }

        private void BindResourceUIFactory()
        {
            Container
                .Bind<IResourceUIFactory>()
                .To<ResourceUIFactory>()
                .AsSingle()
                .WithArguments(resourceUI, container, resourcesDatabase);
        }

        private void BindInventoryUI()
        {
            Container.Bind<IInventoryUI>().FromInstance(inventoryUI).AsSingle();
        }
    }
}
