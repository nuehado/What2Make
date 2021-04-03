using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What2Make_RazorPages.Models.Read
{
    public class RecipeAndIngredientsModel
    {
        public RecipeModel Recipe { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
    }
}
