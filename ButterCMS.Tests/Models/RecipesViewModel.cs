using System.Collections.Generic;

namespace ButterCMS.Tests.Models
{
    public class RecipesViewModel
    {
        public List<RecipeViewModel> Recipes { get; set; }
        public int? PreviousPageNumber { get; set; }
        public int? NextPageNumber { get; set; }
        public int PagesCount { get; set; }
    }
}
