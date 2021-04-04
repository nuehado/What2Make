using System.ComponentModel.DataAnnotations;

namespace What2Make_RazorPages.Models.Create
{
    public class CreateIngredientModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter ingredient name")]
        [MaxLength(50, ErrorMessage = "Ingredient name too long")]
        public string IngredientName { get; set; }

        [Required(ErrorMessage = "Enter quantity for ingredient")]
        [MaxLength(10, ErrorMessage = "Quantity text too long")]
        public string Quantity { get; set; }
    }
}