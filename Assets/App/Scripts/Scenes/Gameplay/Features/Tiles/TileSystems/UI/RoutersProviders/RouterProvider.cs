using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.Routers;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.RoutersProviders
{
    public class RouterProvider
    {
        private DiContainer diContainer;

        private SystemsRoutersDatabase database;

        public ISystemRouter GetSystemRouter(TileSystem system)
        {
            return database.Routers[system];
        }
    }
}