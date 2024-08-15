using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.General.Effectors
{
    public interface IEffectorVisual
    {
        void Setup(Tile tile);
        void Setup(Effector effector);
        void Cleanup();
    }
}