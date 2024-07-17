using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Factories
{
    public class SystemUIFactory : ISystemUIFactory
    {
        private SystemsUIDatabase database;
        
        
        public SystemUIFactory(SystemsUIDatabase database)
        {
            this.database = database;
        }

        public SystemUI GetSystemUI(TileSystem system)
        {
            if (database.SystemsUIs.TryGetValue(system, out var systemUI))
            {
                return systemUI;
            }

            Debug.LogError($"There is no UI for such System: ${system}");
            return null;
        }
    }
}