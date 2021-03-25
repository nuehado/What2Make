using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using What2MakeAPI.Controllers.ModelValidation;
using What2MakeAPI.Models;

namespace What2MakeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeData _recipeData;
        private readonly IIngredientData _ingredientData;

        public RecipeController(IRecipeData recipeData, IIngredientData ingredientData)
        {
            _recipeData = recipeData;
            _ingredientData = ingredientData;
        }


        /*post action for inserting a new recipe, it's ingredients, and building their many-many relatonship. JSON input should have following raw format:
            {
            "Recipe":{
                "RecipeName": "recipename-postman",
                "Description": "description-postman",
                "Instructions": "instructions-postman"
            },

            "IngredientList":[
                {
                    "IngredientName": "ingredientName1-postman",
                    "Quantity": "quant1"
                },
                {
                    "IngredientName": "ingredientName2-postman",
                    "Quantity": "quant2"
                }
            ]
            }
         */
        [HttpPost]
        [ActionName("Create")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(RecipeAndIngredientsModel fullRecipe)
        {
            int id = await _recipeData.CreateRecipe(fullRecipe.Recipe);

            foreach (var ingredient in fullRecipe.IngredientList)
            {
                await _ingredientData.CreateIngredient(ingredient, id);
            }

            return Ok(new { Id = id });
        }


        /*get command for reading an existing recipe and it's ingredients. api url should be of format:
        ServerAddress/api/recipe/#
        where # is the Id of the recipe to get
        */
        [HttpGet("{id}")]
        [ActionName("Load")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var recipe = await _recipeData.GetRecipeById(id);

            if (recipe != null)
            {
                var ingredients = await _ingredientData.GetIngredientsByRecipieId(id);

                RecipeAndIngredientsModel fullRecipe = new RecipeAndIngredientsModel { Recipe = recipe, IngredientList = ingredients };
                return Ok(fullRecipe);
            }
            else
            {
                return NotFound();
            }

        }
        /*get command option 1 for searching for recipes from a list of 1-5 ingredients.
        This approach uses primative url rout attributes for each ingredientName string. route should be of format:
        ServerAddress/api/Recipe/search/mustard/ketchup/romain%20lettuce/n/n
        incrementing up to 5 ingredients
        */
        [HttpGet("{Ingredient1}/{Ingredient2}/{Ingredient3}/{Ingredient4}/{Ingredient5}")]
        [ActionName("Search")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string Ingredient1 = null, string Ingredient2 = null, string Ingredient3 = null, string Ingredient4 = null, string Ingredient5 = null)
        {
            if (string.IsNullOrWhiteSpace(Ingredient1))
            {
                return BadRequest();
            }

            var recipes = await _recipeData.SearchRecipiesByIngredient(Ingredient1, Ingredient2, Ingredient3, Ingredient4, Ingredient5);

            if (recipes.Count < 1)
            {
                return NotFound();
            }
            else
            {
                return Ok(recipes);
            }
            
        }
        /*get command option 2 for searching for recipes from a list of 1-5 ingredients.
        This approach uses a dto object to define the route based off an input model (can be automated with QueryHelpers in UI layer). route should be of format:
        ServerAddress/api/Recipe/search2/ingredients?Ingredient1=mustard&&Ingredient2=oil&&Ingredient3=romain%20lettuce
        incrementing up to 5 ingredients
        */
        [HttpGet("ingredients")]
        [ActionName("Search2")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] IngredientsSearchModel ingredients)
        {
            var recipes = await _recipeData.SearchRecipiesByIngredient(ingredients.Ingredient1, ingredients.Ingredient2, ingredients.Ingredient3, ingredients.Ingredient4, ingredients.Ingredient5);

            return Ok(recipes);
        }
    }
}
