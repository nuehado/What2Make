﻿using DataLibrary.DbAccess;
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
            return _dbAccess.SaveData("dbo.spIngredients_CreateNew", new { IngredientName = ingredient.IngredientName, Quantity = ingredient.Quantity, RecipeId = recipeId }, _connectionStringName);
        }

        public Task<List<IngredientFullModel>> GetIngredientsByRecipieId(int recipeId)
        {
            return _dbAccess.LoadData<IngredientFullModel, dynamic>("spIngredients_GetByRecipeId", new { RecipeId = recipeId }, _connectionStringName);
        }
    }
}