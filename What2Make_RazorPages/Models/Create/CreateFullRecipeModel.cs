using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What2Make_RazorPages.Models.Create
{
    public class CreateFullRecipeModel
    {
        [BindProperty]
        public CreateRecipeModel Recipe { get; set; }

        [BindProperty]
        public List<CreateIngredientModel> Ingredients { get; set; } = new List<CreateIngredientModel>();
    }
}
