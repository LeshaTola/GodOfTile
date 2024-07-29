namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.Providers
{
    public interface IResourceEarnerUIProvider
    {
        ResourceEarnerUI GetSystem(ResourceEarner tileSystem);
    }
}