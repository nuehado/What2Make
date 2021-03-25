using DataLibrary.DbAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using System.Data;

namespace DataLibrary.DataAccess
{
    public class RecipeData : IRecipeData
    {
        private readonly IDbAccess _dbAccess;
        private readonly string _connectionStringName;

        public RecipeData(IDbAccess dbAccess, ConnectionStringData connectionStringData)
        {
            _dbAccess = dbAccess;
            _connectionStringName = connectionStringData.SqlConnectionStringName;
        }

        public Task<List<RecipeSearchResultModel>> SearchRecipiesByIngredient(string Ingredient1, string Ingredient2 = null, string Ingredient3 = null, string Ingredient4 = null, string Ingredient5 = null)
        {
            return _dbAccess.LoadData<RecipeSearchResultModel, dynamic>("dbo.spRecipies_IngredientsSearch",
                                                                  new { Ingredient1, Ingredient2, Ingredient3, Ingredient4, Ingredient5 },
                                                                  _connectionStringName);
        }

        public async Task<int> CreateRecipe(RecipeModel recipe)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("RecipeName", recipe.RecipeName);
            p.Add("Description", recipe.Description);
            p.Add("Instructions", recipe.Instructions);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dbAccess.SaveData("dbo.spRecipies_CreateNew", p, _connectionStringName);

            return p.Get<int>("Id");
        }

        public async Task<RecipeModel> GetRecipeById(int orderId)
        {
            var recipe = await _dbAccess.LoadData<RecipeModel, dynamic>("dbo.spRecipies_ReadById", new { Id = orderId }, _connectionStringName);

            return recipe.FirstOrDefault();
        }
    }
}
