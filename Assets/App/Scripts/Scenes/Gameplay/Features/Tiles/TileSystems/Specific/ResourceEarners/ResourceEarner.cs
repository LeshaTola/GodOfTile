using System;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
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
        public override SystemUI TileUI => tileUI;

        public float AmountPerSecond
        {
            get => amountPerSecond;
            set => amountPerSecond = value;
        }
    }

    public class ResourceEarner : TileSystem
    {
        [SerializeField] private ResourceEarnerSystemData data;

        private IResourceEarnerService resourceEarnerService;

        public ResourceEarner(Tile parentTile,
            ResourceEarnerSystemData data, IResourceEarnerService resourceEarnerService,
            ResourceEarnerUIProvider resourceEarnerUIProvider) : base(parentTile)
        {
            this.data = data;
            this.resourceEarnerService = resourceEarnerService;

            SystemUIProvider = resourceEarnerUIProvider;
        }

        public override TileSystemData Data => data;

        public override void Start()
        {
            base.Start();
            resourceEarnerService.AddResourceEarnerSystem(this);
        }

        public override void Stop()
        {
            base.Stop();
            resourceEarnerService.RemoveResourceEarnerSystem(this);
        }
    }
}