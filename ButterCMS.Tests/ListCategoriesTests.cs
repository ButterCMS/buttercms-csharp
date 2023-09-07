using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{

    [TestFixture]
    [Category("ListCategories")]
    public class ListCategoriesTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void ListCategories_ShouldReturnListOfCategoriesWithoutPosts()
        {
            butterClient.MockSuccessfullCategoriesResponse();

            var response = butterClient.ListCategories();
            Assert.IsNotNull(response);

            var category = response.FirstOrDefault();
            Assert.AreEqual(CategoriesMocks.Category.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.Category.Slug, category.Slug);
            Assert.IsNull(category.RecentPosts);
        }

        [Test]
        public async Task ListCategoriesAsync_ShouldReturnListOfCategoriesWithoutPosts()
        {
            butterClient.MockSuccessfullCategoriesResponse();

            var response = await butterClient.ListCategoriesAsync();
            Assert.IsNotNull(response);

            var category = response.FirstOrDefault();
            Assert.AreEqual(CategoriesMocks.Category.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.Category.Slug, category.Slug);
            Assert.IsNull(category.RecentPosts);
        }

        [Test]
        public void ListCategories_ShouldReturnListOfCategoriesWithPosts()
        {
            butterClient.MockSuccessfullCategoriesResponseWithPosts();

            var response = butterClient.ListCategories(true);
            Assert.IsNotNull(response);

            var category = response.FirstOrDefault();
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.Slug, category.Slug);
            Assert.IsNotEmpty(category.RecentPosts);
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.RecentPosts.FirstOrDefault().Slug, category.RecentPosts.FirstOrDefault().Slug);
        }

        [Test]
        public async Task ListCategoriesAsync_ShouldReturnListOfCategoriesWithPosts()
        {
            butterClient.MockSuccessfullCategoriesResponseWithPosts();

            var response = await butterClient.ListCategoriesAsync(true);
            Assert.IsNotNull(response);

            var category = response.FirstOrDefault();
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.Slug, category.Slug);
            Assert.IsNotEmpty(category.RecentPosts);
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.RecentPosts.FirstOrDefault().Slug, category.RecentPosts.FirstOrDefault().Slug);
        }
    }
}
