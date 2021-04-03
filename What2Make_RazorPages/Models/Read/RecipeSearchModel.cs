namespace What2Make_RazorPages.Models.Read
{
    public class RecipeSearchModel
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public int Matches { get; set; }
    }
}