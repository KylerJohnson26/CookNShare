using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookNShareWebApi.Models;

namespace CookNShareWebApi.Repositories
{
    public interface IRecipeRepository
    {
         Task<Boolean> AddRecipe(Recipe recipe);
         Task<Boolean> EditRecipe(Recipe recipe);
         Task<Boolean> DeleteRecipe(int recipeId);
         List<Recipe> GetRecipesByUserId(int userId);
         Task<Recipe> GetRecipeByRecipeId(int recipeId);
    }
}