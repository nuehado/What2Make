using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public interface IRecipeData
    {
        Task<int> CreateRecipe(RecipeModel recipe);
        Task<RecipeModel> GetRecipeById(int orderId);
        Task<List<RecipeSearchResultModel>> SearchRecipesByIngredient(string Ingredient1, string Ingredient2 = null, string Ingredient3 = null, string Ingredient4 = null, string Ingredient5 = null);
        Task<List<RecipeSearchResultModel>> SearchRecipesByName(string recipeName);
        Task<int> UpdateRecipe(RecipeModel recipe);
    }
}