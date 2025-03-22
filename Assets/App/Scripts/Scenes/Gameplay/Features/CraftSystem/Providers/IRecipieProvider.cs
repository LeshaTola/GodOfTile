using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
    public interface IRecipeProvider
    {
        TileConfig GetRecipeResult(List<TileConfig> neighbors, TileConfig tile);
        RecipeSO GetRecipe(TileConfig tile);
    }
}