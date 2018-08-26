using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookNShareWebApi.Dtos;
using CookNShareWebApi.Models;
using CookNShareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CookNShareWebApi.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepo;
        private readonly IUserRepository _userRepo;
        public RecipeController(IRecipeRepository recipeRepository, IUserRepository userRepository)
        {
            _recipeRepo = recipeRepository;
            _userRepo = userRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewRecipe([FromBody] NewRecipeDto recipeToAdd)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepo.GetUserByUserId(recipeToAdd.UserId);

            Recipe recipe = new Recipe()
            {
                User = user,
                UserId = recipeToAdd.UserId,
                Title = recipeToAdd.Title,
                Description = recipeToAdd.Description,
                DateCreated = DateTime.Now,
                Steps = recipeToAdd.Steps
            };

            var successful = await _recipeRepo.AddRecipe(recipe);
            if(successful)
                return Ok();
            
            return StatusCode(500);
        }

        [HttpGet("allforuser")]
        public IActionResult GetRecipesByUserId(int userId) 
        {
            var recipes = _recipeRepo.GetRecipesByUserId(userId);

            if(recipes.Any())
                return Ok(recipes);

            return BadRequest();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditRecipe([FromBody] Recipe recipe)
        {
            var successful = await _recipeRepo.EditRecipe(recipe);
            
            if(successful)
                return Ok();
            
            return BadRequest();
        }

        [HttpGet("getRecipe")]
        public IActionResult GetRecipeById(int recipeId)
        {
            var recipe = _recipeRepo.GetRecipeByRecipeId(recipeId);
            
            return Ok(recipe);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var successful = await _recipeRepo.DeleteRecipe(recipeId);
            
            if(successful) 
                return Ok();

            return BadRequest();
        }

    }
}