using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class IngredientCreateModel
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string Quantity { get; set; }
        public int RecipeId { get; set; }
    }
}
