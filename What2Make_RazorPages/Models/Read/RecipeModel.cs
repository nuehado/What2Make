using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What2Make_RazorPages.Models.Read
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
    }
}
