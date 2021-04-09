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
        RecipeSearchListModel search;

        public RecipeController(IRecipeData recipeData, IIngredientData ingredientData)
        {
            _recipeData = recipeData;
            _ingredientData = ingredientData;
            search = new RecipeSearchListModel();
        }


        /*post action for inserting a new recipe, it's ingredients, and building their many-many relatonship. JSON body should have following raw format:
            {
            "Recipe":{
                "RecipeName": "recipename-postman",
                "Description": "description-postman",
                "Instructions": "instructions-postman"
            },

            "Ingredients":[
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

            if (id == 0)
            {
                return BadRequest(new DuplicateWaitObjectException());
            }

            foreach (var ingredient in fullRecipe.Ingredients)
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

                RecipeAndIngredientsModel fullRecipe = new RecipeAndIngredientsModel { Recipe = recipe, Ingredients = ingredients };
                return Ok(fullRecipe);
            }
            else
            {
                return NotFound();
            }

        }
        /*update command for updating a recipie's:
         * name
         * description
         * instructions
         * ingredient quantites
         * AND
         * adding new ingredients

         JSON body should have following raw format:
         {
            "Recipe":{
                "Id": 7015,
                "RecipeName": "postman name",
                "Description": "postman description",
                "Instructions": "postman instructions"
            },

            "UpdatedIngredients":[
                {
                    "Id": 6064,
                    "Quantity": "postman 1"
                },
                {
                    "Id": 6065,
                    "Quantity": "postman 2"
                }
            ],

            "NewIngredients":[
                {
                    "IngredientName": "carrots",
                    "Quantity": "postman 3"
                },
                {
                    "IngredientName": "postman ingredient",
                    "Quantity": "postman 4"
                }
            ]
            }
         */
        [HttpPost]
        [ActionName("Update")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(RecipeIngredientsUpdateModel updatedRecipe)
        {
            await _recipeData.UpdateRecipe(updatedRecipe.Recipe);

            foreach (var ingredient in updatedRecipe.UpdatedIngredients)
            {
                await _ingredientData.UpdateIngredientQuantity(updatedRecipe.Recipe.Id, ingredient);
            }

            foreach (var ingredient in updatedRecipe.NewIngredients)
            {
                await _ingredientData.CreateIngredient(ingredient, updatedRecipe.Recipe.Id);
            }

            foreach (var recipeIngredient in updatedRecipe.RemovedIngredients)
            {
                await _ingredientData.DeleteRecipeIngredient(updatedRecipe.Recipe.Id, recipeIngredient.IngredientId);
                
            }

            return Ok(new { updatedRecipe.Recipe.Id });
        }


        /*get command for searching for recipes from a list of 1-5 ingredients.
        This approach uses a dto object to define the route based off an input model (can be automated with QueryHelpers in UI layer). route should be of format:
        ServerAddress/api/Recipe/Search/ingredients?Ingredient1=mustard&&Ingredient2=oil&&Ingredient3=romain%20lettuce
        incrementing up to 5 ingredients
        */
        [HttpGet("ingredients")]
        [ActionName("Search")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] List<string> ingredients)
        {
            if (string.IsNullOrWhiteSpace(ingredients[0]))
            {
                return BadRequest();
            }

            while (ingredients.Count < 5)
            {
                ingredients.Add("");
            }

            search.recipes = await _recipeData.SearchRecipesByIngredient(ingredients[0], ingredients[1], ingredients[2], ingredients[3], ingredients[4]);

            if (search.recipes.Count < 1)
            {
                return NotFound();
            }
            else
            {
                return Ok(search);
            }
        }

        [HttpGet("{recipeName}")]
        [ActionName("NameSearch")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string recipeName)
        {
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                return BadRequest();
            }
            search.recipes = await _recipeData.SearchRecipesByName(recipeName);

            if (search.recipes.Count < 1)
            {
                return NotFound();
            }
            else
            {
                return Ok(search);
            }
        }
    }


}
