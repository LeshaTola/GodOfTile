namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    public interface IResourceEarnerService
    {
        void AddResourceEarnerSystem(string resourceName, float amountPerSecond);
        void RemoveResourceEarnerSystem(string resourceName, float amountPerSecond);
    }
}