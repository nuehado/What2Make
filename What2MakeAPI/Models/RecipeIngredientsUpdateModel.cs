using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What2MakeAPI.Models
{
    public class RecipeIngredientsUpdateModel
    {
        public RecipeModel Recipe { get; set; }
        public List<IngredientQuantityUpdateModel> UpdatedIngredients { get; set; }
        public List<IngredientFullModel> NewIngredients { get; set; }
        public List<RecipeIngredientModel> RemovedIngredients { get; set; }

    }
}
