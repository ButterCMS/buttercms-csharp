namespace ButterCMS.Tests.Models
{
    public class RecipeViewModel
    {
        public string Category { get; set; }
        public string RecipeName { get; set; }
        public string MainIngredient { get; set; }
        public double EstimatedCookingTimeInMinutes { get; set; }
        public string IngredientList { get; set; }
        public string Instructions { get; set; }
    }
}
