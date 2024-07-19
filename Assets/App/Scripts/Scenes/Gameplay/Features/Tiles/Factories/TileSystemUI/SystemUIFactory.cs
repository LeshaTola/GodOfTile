using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Factories.TileSystemUI
{
    public class SystemUIFactory : ISystemUIFactory
    {
        private DiContainer diContainer;

        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            if (tileSystem.Data.TileSystemUI == null)
            {
                return null;
            }

            var newSystemUI = Object.Instantiate(tileSystem.Data.TileSystemUI);
            return newSystemUI;
        }
    }
}