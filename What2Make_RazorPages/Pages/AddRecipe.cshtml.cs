using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Make_RazorPages.Models.Create;

namespace What2Make_RazorPages.Pages
{
    public class AddRecipeModel : PageModel
    {
        [BindProperty]
        public CreateFullRecipeModel CreateFullRecipeModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ErrorString { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        public AddRecipeModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                ErrorString = "Recipe input was invalid. Please try again.";
                return Content(ErrorString);
            }

            var client = _clientFactory.CreateClient("w2m");

            try
            {
                var response = await client.PostAsJsonAsync("recipe/create", CreateFullRecipeModel);
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadFromJsonAsync<RecipeCreateResultsModel>();
                    ErrorString = null;
                    return RedirectToPage("./recipe", new { contents.Id });
                }
                else
                {
                    ErrorString = $"{CreateFullRecipeModel.Recipe.RecipeName} already exists in What2Make";
                    return RedirectToPage("./AddRecipe", new { ErrorString });
                }
                
            }
            catch (Exception ex)
            {
                ErrorString = $"There was an error saving your recipe: {ex.Message}";
                return RedirectToPage("./AddRecipe", new { ErrorString });
            }
        }
    }
}
