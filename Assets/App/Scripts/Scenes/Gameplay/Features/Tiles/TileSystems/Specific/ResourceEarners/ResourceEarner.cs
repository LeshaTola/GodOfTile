using System;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    [Serializable]
    public class ResourceEarnerSystemData : TileSystemData
    {
        [SerializeField] private ResourceEarnerUIProvider systemUIProvider;
        [SerializeField] private ResourceConfig resource;
        [SerializeField] private float amountPerSecond;

        public ResourceConfig Resource => resource;
        public override ISystemUIProvider SystemUIProvider => systemUIProvider;

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

        public ResourceEarner(Tile parentTile, ResourceEarnerSystemData data,
            IResourceEarnerService resourceEarnerService) : base(parentTile)
        {
            this.data = data;
            this.resourceEarnerService = resourceEarnerService;
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