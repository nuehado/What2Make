using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class RecipeSearchResultModel
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public int Matches { get; set; }
    }
}
