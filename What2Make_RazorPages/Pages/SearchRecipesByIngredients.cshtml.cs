using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Make_RazorPages.Models.Read;

namespace What2Make_RazorPages.Pages
{
    public class SearchRecipesByIngredientsModel : PageModel
    {
        [BindProperty]
        public RecipeSearchListModel recipeSearchResults { get; set; }

        [BindProperty]
        public string[] Ingredients { get; set; } = new string[5];

        [BindProperty(SupportsGet = true)]
        public string IngredientList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ErrorString { get; set; }

        [BindProperty]
        public int recipeId { get; set; }

        public string errorString;

        private readonly IHttpClientFactory _clientFactory;

        public SearchRecipesByIngredientsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            if (IngredientList != null)
            {
                Ingredients = IngredientList.Split('-');
            }
            if (Ingredients.Count() > 0) //TODO: set up data validation and change to ModelState.IsValid == true
            {
                if (Ingredients[0] != null)
                {
                    var client = _clientFactory.CreateClient("w2m");
                    try
                    {
                        recipeSearchResults = await client.GetFromJsonAsync<RecipeSearchListModel>($"recipe/search/ingredients?Ingredient1={Ingredients[0]}&&Ingredient2={Ingredients[1]}&&Ingredient3={Ingredients[2]}&&Ingredient4={Ingredients[3]}&&Ingredient5={Ingredients[4]}");
                        errorString = null;
                    }
                    catch (Exception)
                    {

                        errorString = $"No recipes were found with those ingredients";
                    }
                }
            }
            else
            {
                Ingredients = new string[5];
            }
        }

        public RedirectToPageResult OnPost()
        {
            string ingredients = string.Join('-', Ingredients);
            return RedirectToPage("./SearchRecipesByIngredients", new { IngredientList = ingredients, ErrorString = errorString });
        }

        public RedirectToPageResult OnPostGoToRecipe()
        {
            return RedirectToPage("./Recipe", new { Id = recipeId });
        }
    }
}

