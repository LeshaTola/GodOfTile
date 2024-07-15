using System.Collections.Generic;
using System.Linq;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using CraftingSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
    public class RecipeProvider : IRecipeProvider
    {
        public TileConfig GetRecipe(List<TileConfig> neighbors, TileConfig tile)
        {
            var recipes = Resources.LoadAll<RecipeSO>("Recipes");

            var recipesForOrigin = recipes.Where(r => r.Original.Id.Equals(tile.Id)).ToList();
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
    }
}