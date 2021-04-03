using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Make_RazorPages.Models.Read;

namespace What2Make_RazorPages.Pages
{
    public class RecipeModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } = 13;

        public RecipeAndIngredientsModel recipe;
        public string errorString;

        private readonly IHttpClientFactory _clientFactory;

        public RecipeModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet(int Id)
        {
            var client = _clientFactory.CreateClient("w2m");

            try
            {
                recipe = await client.GetFromJsonAsync<RecipeAndIngredientsModel>($"recipe/load/{Id}");
                errorString = null;
            }
            catch (Exception ex)
            {
                errorString = $"There was an error loading the recipe:\r\n{ex.Message}";
            }
        }
    }
}
