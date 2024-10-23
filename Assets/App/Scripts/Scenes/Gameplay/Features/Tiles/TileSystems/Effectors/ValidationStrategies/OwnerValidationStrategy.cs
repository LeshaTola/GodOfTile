using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies
{
    public class OwnerValidationStrategy : IValidationStrategy
    {
        private Tile owner;

        public OwnerValidationStrategy(Tile owner)
        {
            this.owner = owner;
        }

        public List<Tile> ValidateTiles(List<Tile> tiles)
        {
            return new() {owner};
        }

        public List<TileSystem> ValidateSystems(List<Tile> tiles)
        {
            return owner.Config.ActiveSystems;
        }
    }
}