using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific
{
    [Serializable]
    public class ResourceEarnerSystemData : TileSystemData
    {
        [SerializeField] private ResourcesDatabase resourcesDatabase;

        [SerializeField]
        [ValueDropdown(nameof(GetResources))]
        private string resource;

        [SerializeField] private int amount;
        [SerializeField] private int cooldown;

        public string Resource => resource;
        public int Amount => amount;
        public int Cooldown => cooldown;

        public IEnumerable<string> GetResources()
        {
            if (resourcesDatabase == null)
            {
                return null;
            }

            return resourcesDatabase.Resources.Select(x => x.ResourceName);
        }
    }

    public class ResourceEarner : TileSystem
    {
        [SerializeField] private ResourceEarnerSystemData data;

        private float timer;

        private IInventorySystem inventorySystem;

        public ResourceEarner(IInventorySystem inventorySystem, ResourceEarnerSystemData data)
        {
            this.inventorySystem = inventorySystem;
            this.data = data;
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
    }
}