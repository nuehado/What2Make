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

        public string errorString;

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
                string message = "Recipe input was invalid. Please try again.";
                return Content(message);
            }

            var client = _clientFactory.CreateClient("w2m");

            try
            {
                var response = await client.PostAsJsonAsync("recipe/create", CreateFullRecipeModel);
                var contents = await response.Content.ReadFromJsonAsync<RecipeCreateResultsModel>();
                errorString = null;
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
