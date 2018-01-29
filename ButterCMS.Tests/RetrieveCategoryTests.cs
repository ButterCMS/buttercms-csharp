using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrieveCategoryTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void RetrieveCategory_ShouldReturnCategoryWithoutPosts()
        {
            var response = butterClient.RetrieveCategory("test-category");
            Assert.IsNotNull(response);
            Assert.IsNull(response.RecentPosts);
        }

        [Test]
        public async Task RetrieveCategoryAsync_ShouldReturnCategoryWithoutPosts()
        {
            var response = await butterClient.RetrieveCategoryAsync("test-category");
            Assert.IsNotNull(response);
            Assert.IsNull(response.RecentPosts);
        }

        [Test]
        public void RetrieveCategory_ShouldReturnCategoryWithPosts()
        {
            var response = butterClient.RetrieveCategory("test-category", true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.RecentPosts);
        }

        [Test]
        public async Task RetrieveCategoryAsync_ShouldReturnCategoryWithPosts()
        {
            var response = await butterClient.RetrieveCategoryAsync("test-category", true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.RecentPosts);
        }

        [Test]
        public void RetrieveCategory_ShouldReturnNull()
        {
            var response = butterClient.RetrieveCategory(categorySlug: "not-a-real-category");
            Assert.IsNull(response);
        }

        [Test]
        public async Task RetrieveCategoryAsync_ShouldReturnNull()
        {
            var response = await butterClient.RetrieveCategoryAsync(categorySlug: "not-a-real-category");
            Assert.IsNull(response);
        }
    }
}
