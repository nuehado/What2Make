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
    public class SearchRecipesByNameModel : PageModel
    {


        [BindProperty(SupportsGet = true)]
        [MaxLength(50, ErrorMessage = "Recipe name too long")]
        public string RecipeName { get; set; }
        [BindProperty]
        public int RecipeId { get; set; }

        [BindProperty]
        public RecipeSearchGroupModel RecipeSearchResults { get; set; }


        [BindProperty(SupportsGet = true)]
        public string ErrorString { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        public SearchRecipesByNameModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            if (RecipeName != null)
            {
                var client = _clientFactory.CreateClient("w2m");
                try
                {
                    RecipeSearchResults = await client.GetFromJsonAsync<RecipeSearchGroupModel>($"recipe/NameSearch/{RecipeName}");
                    ErrorString = null;
                }

                catch (Exception)
                {

                    ErrorString = $"No recipes found containing name: {RecipeName}";
                }
            }
        }

        public RedirectToPageResult OnPost()
        {

            return RedirectToPage("./SearchRecipesByName", new { RecipeName, ErrorString});
        }

        public RedirectToPageResult OnPostGoToRecipe()
        {
            return RedirectToPage("./Recipe", new { Id = RecipeId });
        }
    }
}
