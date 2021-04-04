using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace What2Make_RazorPages.Models.Create
{
    public class CreateRecipeModel
    {
        public int Id { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter recipe name")]
        [MaxLength(50, ErrorMessage = "Recipe name is too long")]
        public string RecipeName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter recipe decription")]
        [MaxLength(200, ErrorMessage = "Recipe decription is too long")]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter recipe instructions")]
        [MaxLength(2000, ErrorMessage = "Recipe instructions are too long")]
        public string Instructions { get; set; }
    }
}