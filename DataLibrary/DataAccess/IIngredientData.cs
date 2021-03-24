using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public interface IIngredientData
    {
        Task CreateIngredient(IngredientCreateModel ingredient);
        Task<List<IngredientFullModel>> GetIngredientsByRecipieId(int recipeId);
    }
}