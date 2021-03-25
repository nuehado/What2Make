using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using What2MakeAPI.Controllers.ModelValidation;

namespace What2MakeAPI.Controllers
{
    [Route("api/[controller]")]
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
    }
}
