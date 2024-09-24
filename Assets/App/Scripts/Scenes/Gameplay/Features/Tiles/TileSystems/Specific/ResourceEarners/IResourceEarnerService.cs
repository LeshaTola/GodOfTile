namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    public interface IResourceEarnerService
    {
        bool Active { get; set; }

        void AddResourceEarnerSystem(ResourceEarner resourceEarner);
        void RemoveResourceEarnerSystem(ResourceEarner resourceEarner);
    }
}