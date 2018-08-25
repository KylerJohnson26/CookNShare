using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookNShareWebApi.Data;
using CookNShareWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CookNShareWebApi.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddRecipe(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges > 0;
        }

        public async Task<bool> DeleteRecipe(int recipeId)
        {
            Recipe recipe = await GetRecipeByRecipeId(recipeId);
            _context.Recipes.Remove(recipe);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges > 0;
        }

        public async Task<bool> EditRecipe(Recipe editedRecipe)
        {
            _context.Update(editedRecipe);
            var numChanges = await _context.SaveChangesAsync();
            return numChanges > 0;
        }

        public async Task<Recipe> GetRecipeByRecipeId(int recipeId)
        {
            return await _context.Recipes.FirstOrDefaultAsync(x => x.Id == recipeId);
        }

        public List<Recipe> GetRecipesByUserId(int userId)
        {
            return _context.Recipes.Where(x => x.UserId == userId).ToList();
        }
    }
}