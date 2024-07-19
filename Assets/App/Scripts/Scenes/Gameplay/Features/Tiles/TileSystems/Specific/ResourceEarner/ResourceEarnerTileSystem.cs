using System;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarner.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific
{
    [Serializable]
    public class ResourceEarnerSystemData : TileSystemData
    {
        [SerializeField] private ResourceEarnerSystemUI tileSystemUI;
        [SerializeField] private ResourceConfig resource;
        [SerializeField] private int amount;
        [SerializeField] private int cooldown;

        public string Resource => resource.ResourceName;
        public int Amount => amount;
        public int Cooldown => cooldown;
        public override ResourceEarnerSystemUI TileSystemUI => tileSystemUI;
    }

    public class ResourceEarnerTileSystem : TileSystem
    {
        [SerializeField] private ResourceEarnerSystemData data;

        private float timer;

        private IResourceEarnerTileSystemUIProvider resourceEarnerTileSystemUIProvider;
        private IInventorySystem inventorySystem;

        public ResourceEarnerTileSystem(ResourceEarnerSystemData data,
            IResourceEarnerTileSystemUIProvider resourceEarnerTileSystemUIProvider,
            IInventorySystem inventorySystem)
        {
            this.data = data;

            this.resourceEarnerTileSystemUIProvider = resourceEarnerTileSystemUIProvider;
            this.inventorySystem = inventorySystem;
        }

        public override TileSystemData Data => data;

        public override void Start()
        {
            base.Start();

            timer = data.Cooldown;
        }

        public override void Update()
        {
            base.Update();

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = data.Cooldown;
                inventorySystem.ChangeRecourseAmount(data.Resource, data.Amount);
            }
        }

        public override SystemUI GetSystemUI()
        {
            return resourceEarnerTileSystemUIProvider.GetSystem(this);
        }
    }
}