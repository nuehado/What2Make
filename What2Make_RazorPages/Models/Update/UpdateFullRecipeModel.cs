using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using What2Make_RazorPages.Models.Create;

namespace What2Make_RazorPages.Models.Update
{
    public class UpdateFullRecipeModel
    {
        public UpdateRecipeInfoModel Recipe { get; set; } = new UpdateRecipeInfoModel();
        public List<UpdateIngredientQuantityModel> UpdatedIngredients { get; set; } = new List<UpdateIngredientQuantityModel>();
        public List<CreateIngredientModel> NewIngredients { get; set; } = new List<CreateIngredientModel>();
    }
}
