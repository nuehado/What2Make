using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public interface IIngredientData
    {
        Task CreateIngredient(IngredientFullModel ingredient, int recipeId);
        Task<List<IngredientFullModel>> GetIngredientsByRecipieId(int recipeId);
        Task<int> UpdateIngredientQuantity(int recipeId, IngredientQuantityUpdateModel ingredient);
    }
}