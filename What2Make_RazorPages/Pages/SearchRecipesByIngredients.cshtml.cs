using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public RecipeSearchGroupModel RecipeSearchResults { get; set; }

        [BindProperty(SupportsGet = true)]
        [MaxLength(50, ErrorMessage = "Ingredient name too long")]
        public List<string> Ingredients { get; set; } = new List<string>();

        [BindProperty(SupportsGet = true)]
        public string ErrorString { get; set; }

        [BindProperty]
        public int RecipeId { get; set; }

        public string errorString;

        private readonly int maxSearchFields = 5;

        private readonly IHttpClientFactory _clientFactory;

        public SearchRecipesByIngredientsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            if (Ingredients.Count() > 0) //TODO: set up data validation and change to ModelState.IsValid == true
            {
                if (Ingredients[0] != null)
                {
                    while (Ingredients.Count < maxSearchFields)
                    {
                        Ingredients.Add("");
                    }

                    string searchParams = "ingredients?";
                    for (int i = 0; i < maxSearchFields; i++)
                    {
                         searchParams += ($"Ingredients[{i}]={Ingredients[i]}&");
                    }

                    var client = _clientFactory.CreateClient("w2m");
                    try
                    {
                        RecipeSearchResults = await client.GetFromJsonAsync<RecipeSearchGroupModel>($"recipe/search/{searchParams}");
                        errorString = null;
                    }
                    catch (Exception)
                    {

                        errorString = $"No recipes found for any of those ingredients";
                    }
                }
            }
            else
            {
                Ingredients = new List<string>();
            }
        }

        public RedirectToPageResult OnPost()
        {
            
            return RedirectToPage("./SearchRecipesByIngredients", new { Ingredients, ErrorString = errorString });
        }

        public RedirectToPageResult OnPostGoToRecipe()
        {
            return RedirectToPage("./Recipe", new { Id = RecipeId });
        }
    }
}

