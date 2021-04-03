using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What2Make_RazorPages.Models.Read
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string Quantity { get; set; }
    }
}
