using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarner.UI.Providers
{
    public interface IResourceEarnerTileSystemUIProvider
    {
        ResourceEarnerSystemUI GetSystem(ResourceEarnerTileSystem tileSystem);
    }
}