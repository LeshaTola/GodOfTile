using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI
{
    public class SystemUIFactory : ISystemUIFactory
    {
        private DiContainer diContainer;

        public SystemUI GetSystemUI(TileSystems.TileSystem tileSystem)
        {
            if (tileSystem.Data.TileUI == null)
            {
                return null;
            }

            var newSystemUI = Object.Instantiate(tileSystem.Data.TileUI);
            return newSystemUI;
        }
    }
}