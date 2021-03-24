using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
    }
}
