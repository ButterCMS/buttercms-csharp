using ButterCMS.Models;
using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class ReadmePageTypeExamples
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }


        [Test]
        [Ignore("This test is ignored because it is used as an example in the readme. Actual functionality is tested in ListPagesTests.cs")]
        public void ReadmeExample_ListPages()
        {
            int page = 1;
            int pageSize = 10;

            var parameterDict = new Dictionary<string, string>()
            {
                {"page", page.ToString()},
                {"page_size", pageSize.ToString()}
            };

            PagesResponse<RecipePage> recipePages = butterClient.ListPages<RecipePage>("recipe", parameterDict);

            var viewModel = new RecipesViewModel();
            viewModel.PreviousPageNumber = recipePages.Meta.PreviousPage;
            viewModel.NextPageNumber = recipePages.Meta.NextPage;
            viewModel.PagesCount = recipePages.Meta.Count;

            viewModel.Recipes = new List<RecipeViewModel>();
            foreach (Page<RecipePage> recipe in recipePages.Data)
            {
                RecipeViewModel recipeViewModel = new RecipeViewModel();
                recipeViewModel.Category = recipe.Fields.category;
                recipeViewModel.RecipeName = recipe.Fields.recipe_name;
                recipeViewModel.MainIngredient = recipe.Fields.main_ingredient;
                recipeViewModel.EstimatedCookingTimeInMinutes = recipe.Fields.estimated_cooking_time_in_minutes;
                recipeViewModel.IngredientList = recipe.Fields.ingredient_list;
                recipeViewModel.Instructions = recipe.Fields.instructions;

                viewModel.Recipes.Add(recipeViewModel);
            }

            Assert.IsNotNull(viewModel);
        }

        [Test]
        [Ignore("This test is ignored because it is used as an example in the readme. Actual functionality is tested in RetrievePageTests.cs")]
        public void ReadmeExample_RetrievePage()
        {
            var slug = "chicken-soup";

            PageResponse<RecipePage> recipe = butterClient.RetrievePage<RecipePage>("recipe", slug);

            var viewModel = new RecipeViewModel();
            viewModel.Category = recipe.Data.Fields.category;
            viewModel.RecipeName = recipe.Data.Fields.recipe_name;
            viewModel.MainIngredient = recipe.Data.Fields.main_ingredient;
            viewModel.EstimatedCookingTimeInMinutes = recipe.Data.Fields.estimated_cooking_time_in_minutes;
            viewModel.IngredientList = recipe.Data.Fields.ingredient_list;
            viewModel.Instructions = recipe.Data.Fields.instructions;
            viewModel.Updated = recipe.Data.Updated;
            viewModel.Published = recipe.Data.Published;

            Assert.IsNotNull(viewModel);
        }
    }
}
