using System;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    [Serializable]
    public class ResourceEarnerSystemData : TileSystemData
    {
        [SerializeField] private ResourceEarnerUI tileUI;

        [SerializeField] private ResourceConfig resource;
        [SerializeField] private float amountPerSecond;

        public ResourceConfig Resource => resource;
        public float AmountPerSecond => amountPerSecond;
        public override SystemUI TileUI => tileUI;
    }

    public class ResourceEarner : TileSystem
    {
        [SerializeField] private ResourceEarnerSystemData data;

        private IResourceEarnerUIProvider resourceEarnerUIProvider;
        private IResourceEarnerService resourceEarnerService;

        public ResourceEarner(ResourceEarnerSystemData data,
            IResourceEarnerUIProvider resourceEarnerUIProvider,
            IResourceEarnerService resourceEarnerService)
        {
            this.data = data;

            this.resourceEarnerUIProvider = resourceEarnerUIProvider;
            this.resourceEarnerService = resourceEarnerService;
        }

        public override TileSystemData Data => data;

        public override void Start()
        {
            base.Start();
            resourceEarnerService.AddResourceEarnerSystem(data.Resource.ResourceName, data.AmountPerSecond);
        }

        public override void Stop()
        {
            base.Stop();
            resourceEarnerService.RemoveResourceEarnerSystem(data.Resource.ResourceName, data.AmountPerSecond);
        }

        public override SystemUI GetSystemUI()
        {
            return resourceEarnerUIProvider.GetSystem(this);
        }
    }
}