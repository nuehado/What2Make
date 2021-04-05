using System.ComponentModel.DataAnnotations;

namespace What2Make_RazorPages.Models.Update
{
    public class UpdateIngredientQuantityModel
    {
        public int Id { get; set; }

        public string IngredientName { get; set; }

        [Required(ErrorMessage = "Enter quantity for ingredient")]
        [MaxLength(10, ErrorMessage = "Quantity text too long")]
        [Display(Name = "test)")]
        public string Quantity { get; set; }
    }
}