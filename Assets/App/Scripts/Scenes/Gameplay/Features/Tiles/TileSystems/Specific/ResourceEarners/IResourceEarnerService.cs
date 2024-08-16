namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    public interface IResourceEarnerService
    {
        void AddResourceEarnerSystem(ResourceEarner resourceEarner);
        void RemoveResourceEarnerSystem(ResourceEarner resourceEarner);
    }
}