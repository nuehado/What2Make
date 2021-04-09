using DataLibrary.DbAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public class IngredientData : IIngredientData
    {
        private readonly IDbAccess _dbAccess;
        private readonly string _connectionStringName;

        public IngredientData(IDbAccess dbAccess, ConnectionStringData connectionStringData)
        {
            _dbAccess = dbAccess;
            _connectionStringName = connectionStringData.SqlConnectionStringName;
        }

        public Task CreateIngredient(IngredientFullModel ingredient, int recipeId)
        {
            return _dbAccess.SaveData("dbo.spIngredients_CreateNew", new { ingredient.IngredientName, Quantity = ingredient.Quantity, RecipeId = recipeId }, _connectionStringName);
        }

        public Task<List<IngredientFullModel>> GetIngredientsByRecipieId(int recipeId)
        {
            return _dbAccess.LoadData<IngredientFullModel, dynamic>("spIngredients_GetByRecipeId", new { RecipeId = recipeId }, _connectionStringName);
        }

        public async Task<int> UpdateIngredientQuantity(int recipeId, IngredientQuantityUpdateModel ingredient)
        {
            return await _dbAccess.SaveData("dbo.spRecipesIngredients_UpdateQuantity", new { RecipeId = recipeId, IngredientId = ingredient.Id, Quantity = ingredient.Quantity }, _connectionStringName);
        }

        public async Task<int> DeleteRecipeIngredient(int recipeId, int ingredientId)
        {
            return await _dbAccess.SaveData("dbo.spRecipesIngredients_Delete", new { RecipeId = recipeId, IngredientId = ingredientId }, _connectionStringName);
        }
    }
}
