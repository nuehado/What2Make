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

        public IngredientData(IDbAccess dbAccess) //TODO: populate dependency injected _connectionStringName field
        {
            _dbAccess = dbAccess;
        }

        public Task CreateIngredient(IngredientCreateModel ingredient)
        {
            return _dbAccess.SaveData("dbo.pIngredients_CreateNew", new { IngredientName = ingredient.IngredientName, Quantity = ingredient.Quantity, RecipeId = ingredient.RecipeId }, _connectionStringName);
        }

        public Task<List<IngredientFullModel>> GetIngredientsByRecipieId(int recipeId)
        {
            return _dbAccess.LoadData<IngredientFullModel, dynamic>("spIngredients_GetByRecipeId", new { RecipeId = recipeId }, _connectionStringName);
        }
    }
}
