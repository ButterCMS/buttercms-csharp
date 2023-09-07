using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RetrieveCategory")]
    public class RetrieveCategoryTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void RetrieveCategory_ShouldReturnCategoryWithoutPosts()
        {
            butterClient.MockSuccessfullCategoryResponse(CategoriesMocks.Category.Slug);

            var category = butterClient.RetrieveCategory(CategoriesMocks.Category.Slug);
            Assert.IsNotNull(category);
            
            Assert.AreEqual(CategoriesMocks.Category.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.Category.Slug, category.Slug);
            Assert.IsNull(category.RecentPosts);
        }

        [Test]
        public async Task RetrieveCategoryAsync_ShouldReturnCategoryWithoutPosts()
        {
            butterClient.MockSuccessfullCategoryResponse(CategoriesMocks.Category.Slug);

            var category = await butterClient.RetrieveCategoryAsync(CategoriesMocks.Category.Slug);
            Assert.IsNotNull(category);
            
            Assert.AreEqual(CategoriesMocks.Category.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.Category.Slug, category.Slug);
            Assert.IsNull(category.RecentPosts);
        }

        [Test]
        public void RetrieveCategory_ShouldReturnCategoryWithPosts()
        {
            butterClient.MockSuccessfullCategoryResponseWithPosts(CategoriesMocks.CategoryWithPosts.Slug);

            var category = butterClient.RetrieveCategory(CategoriesMocks.Category.Slug, true);
            Assert.IsNotNull(category);
            
            Assert.AreEqual(CategoriesMocks.Category.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.Category.Slug, category.Slug);
            CollectionAssert.IsNotEmpty(category.RecentPosts);
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.RecentPosts.First().Slug, category.RecentPosts.First().Slug);
        }

        [Test]
        public async Task RetrieveCategoryAsync_ShouldReturnCategoryWithPosts()
        {
            butterClient.MockSuccessfullCategoryResponseWithPosts(CategoriesMocks.CategoryWithPosts.Slug);

            var category = await butterClient.RetrieveCategoryAsync(CategoriesMocks.Category.Slug, true);
            Assert.IsNotNull(category);
            
            Assert.AreEqual(CategoriesMocks.Category.Name, category.Name);
            Assert.AreEqual(CategoriesMocks.Category.Slug, category.Slug);
            CollectionAssert.IsNotEmpty(category.RecentPosts);
            Assert.AreEqual(CategoriesMocks.CategoryWithPosts.RecentPosts.First().Slug, category.RecentPosts.First().Slug);
        }

        [Test]
        public void RetrieveCategory_ShouldReturnNull()
        {
            var slug = "not-a-real-category";
            butterClient.MockSuccessfullNullCategoryResponse(slug);

            var response = butterClient.RetrieveCategory(categorySlug: slug);
            Assert.IsNull(response);
        }

        [Test]
        public async Task RetrieveCategoryAsync_ShouldReturnNull()
        {
            var slug = "not-a-real-category";
            butterClient.MockSuccessfullNullCategoryResponse(slug);

            var response = await butterClient.RetrieveCategoryAsync(categorySlug: slug);
            Assert.IsNull(response);
        }
    }
}
