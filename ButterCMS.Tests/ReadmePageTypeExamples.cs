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
                recipeViewModel.Category = recipe.Fields.Category;
                recipeViewModel.RecipeName = recipe.Fields.RecipeName;
                recipeViewModel.MainIngredient = recipe.Fields.MainIngredient;
                recipeViewModel.EstimatedCookingTimeInMinutes = recipe.Fields.EstimatedCookingTimeInMinutes;
                recipeViewModel.IngredientList = recipe.Fields.IngredientList;
                recipeViewModel.Instructions = recipe.Fields.Instructions;

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
            viewModel.Category = recipe.Data.Fields.Category;
            viewModel.RecipeName = recipe.Data.Fields.RecipeName;
            viewModel.MainIngredient = recipe.Data.Fields.MainIngredient;
            viewModel.EstimatedCookingTimeInMinutes = recipe.Data.Fields.EstimatedCookingTimeInMinutes;
            viewModel.IngredientList = recipe.Data.Fields.IngredientList;
            viewModel.Instructions = recipe.Data.Fields.Instructions;
            viewModel.Updated = recipe.Data.Updated;
            viewModel.Published = recipe.Data.Published;

            Assert.IsNotNull(viewModel);
        }
    }
}
