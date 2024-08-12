namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizators
{
    public interface IMapVisualizator
    {
        void Hide();
        void Show();
        void UpdateChunks();
    }
}