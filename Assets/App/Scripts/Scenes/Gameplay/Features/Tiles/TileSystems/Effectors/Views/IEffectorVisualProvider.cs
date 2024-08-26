using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views
{
    public interface IEffectorVisualProvider
    {
        void Setup(Tile tile);
        void Setup(Effector effector);
        void Cleanup();
    }
}