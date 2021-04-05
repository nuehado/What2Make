using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Make_RazorPages.Models.Create;
using What2Make_RazorPages.Models.Read;
using What2Make_RazorPages.Models.Update;

namespace What2Make_RazorPages.Pages
{
    public class UpdateRecipeModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; } = 13;

        public RecipeAndIngredientsModel OriginalRecipe { get; set; }

        [BindProperty]
        public UpdateFullRecipeModel UpdatedRecipe { get; set; } = new UpdateFullRecipeModel();

        public string errorString;

        private readonly IHttpClientFactory _clientFactory;

        public UpdateRecipeModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet(int id)
        {
            var client = _clientFactory.CreateClient("w2m");

            try
            {
                OriginalRecipe = await client.GetFromJsonAsync<RecipeAndIngredientsModel>($"recipe/load/{id}");

                UpdatedRecipe.Recipe.Id = OriginalRecipe.Recipe.Id;
                UpdatedRecipe.Recipe.RecipeName = OriginalRecipe.Recipe.RecipeName;
                UpdatedRecipe.Recipe.Description = OriginalRecipe.Recipe.Description;
                UpdatedRecipe.Recipe.Instructions = OriginalRecipe.Recipe.Instructions;

                foreach (var ingredient in OriginalRecipe.Ingredients)
                {
                    UpdatedRecipe.UpdatedIngredients.Add(new UpdateIngredientQuantityModel() { Id = ingredient.Id, IngredientName = ingredient.IngredientName, Quantity = ingredient.Quantity });
                }

                errorString = null;
            }
            catch (Exception ex)
            {
                errorString = $"There was an error loading the recipe:\r\n{ex.Message}";
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                string message = "Recipe update was invalid. Try again";
                return Content(message);
            }

            var client = _clientFactory.CreateClient("w2m");

            try
            {
                var response = await client.PostAsJsonAsync("recipe/update", UpdatedRecipe);
                var contents = await response.Content.ReadFromJsonAsync<RecipeCreateResultsModel>();
                return RedirectToPage("./recipe", new { contents.Id });
            }
            catch (Exception ex)
            {
                errorString = $"There was an error saving your recipe: {ex.Message}";
                return Page();
            }
        }
    }
}