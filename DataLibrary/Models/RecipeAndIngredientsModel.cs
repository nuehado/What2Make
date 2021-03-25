using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class RecipeAndIngredientsModel
    {
        public RecipeModel Recipe { get; set; }
        public List<IngredientFullModel> IngredientList { get; set; }
    }
}
