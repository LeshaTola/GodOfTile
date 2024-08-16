using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects
{
    public interface IEffect
    {
        IValidationStrategy ValidationStrategy { get; }
        Effector Effector { get; }
        ISystemUIProvider SystemUIProvider { get; }

        void Initialize(Effector effector);
        void AddEffect(TileSystemData tileSystemData);
        void RemoveEffect(TileSystemData tileSystemData);
    }
}