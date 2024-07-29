using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
    public interface IRecipeProvider
    {
        public TileConfig GetRecipe(List<TileConfig> neighbors, TileConfig tile);
    }
}