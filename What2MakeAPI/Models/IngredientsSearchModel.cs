using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What2MakeAPI.Models
{
    public class IngredientsSearchModel
    {
        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; } = null;
        public string Ingredient3 { get; set; } = null;
        public string Ingredient4 { get; set; } = null;
        public string Ingredient5 { get; set; } = null;
    }
}
