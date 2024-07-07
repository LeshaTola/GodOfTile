using System.Linq;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Factories
{
    public class ResourceUIFactory : IResourceUIFactory
    {
        private ResourceUI template;
        private RectTransform container;
        private ResourcesDatabase database;

        public ResourceUIFactory(
            ResourceUI template,
            RectTransform container,
            ResourcesDatabase database
        )
        {
            this.template = template;
            this.container = container;
            this.database = database;
        }

        public IResourceUI GetRecourseUI(string resourceName)
        {
            IResourceUI UI = GameObject.Instantiate(template, container);
            var config = database.Resources.FirstOrDefault(x =>
                x.ResourceName.Equals(resourceName)
            );
            if (config != null)
            {
                UI.Initialize(config.Sprite, config.StartAmount);
            }
            return UI;
        }
    }
}
