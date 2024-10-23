using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Factories;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Inventory;
using App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Resource;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Inventory
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
            BindPlacementCostService();
        }

        private void BindPlacementCostService()
        {
            Container.BindInterfacesTo<PlacementCostService>().AsSingle().NonLazy();
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