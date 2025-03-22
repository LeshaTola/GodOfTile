using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
    public class RecipeProvider : IRecipeProvider
    {
        public TileConfig GetRecipeResult(List<TileConfig> neighbors, TileConfig tile)
        {
            var recipes = GetAllRecipes();

            var recipesForOrigin = GetRecipesByOriginal(tile, recipes);
            recipesForOrigin.Sort((x, y) => y.RequiredTiles.Count.CompareTo(x.RequiredTiles.Count));
            foreach (var recipe in recipesForOrigin)
            {
                var ingredientIds = recipe.RequiredTiles.Select(t => t.Id).ToList();
                var neighborsId = neighbors.Select(t => t.Id).ToList();
                if (
                    ingredientIds.All(x =>
                        neighborsId.Count(y => y.Equals(x)) >= ingredientIds.Count(y => y == x)
                    )
                )
                {
                    return recipe.Result;
                }
            }

            return null;
        }

        public RecipeSO GetRecipe(TileConfig tile)
        {
            var recipes = GetAllRecipes();
            return recipes.FirstOrDefault(x=>x.Result.Id.Equals(tile.Id));
        }

        private static RecipeSO[] GetAllRecipes()
        {
            var recipes = Resources.LoadAll<RecipeSO>("Recipes");
            return recipes;
        }

        private static List<RecipeSO> GetRecipesByOriginal(TileConfig tile, RecipeSO[] recipes)
        {
            return recipes.Where(r => r.Original.Id.Equals(tile.Id)).ToList();
        }
    }
}