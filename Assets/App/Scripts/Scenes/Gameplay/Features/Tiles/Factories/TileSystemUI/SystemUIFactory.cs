using System;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI
{
    public class SystemUIFactory : ISystemUIFactory
    {
        private SystemsUIsDatabase database;

        public SystemUIFactory(SystemsUIsDatabase database)
        {
            this.database = database;
        }

        public T GetSystemUI<T>() where T : SystemUI
        {
            var type = typeof(T);
            return (T)GetSystemUI(type);
        }
        
        public SystemUI GetSystemUI(Type type)
        {
            var systemUI = database.UIs.FirstOrDefault(x => x.GetType().Equals(type));
            if (systemUI == null)
            {
                return null;
            }
            
            var newSystemUI = Object.Instantiate(systemUI);
            return newSystemUI;
        }
    }
}