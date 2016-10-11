using NUnit.Framework;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{

    [TestFixture]
    public class ListCategoriesTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void ListCategories_ShouldReturnListOfCategoriesWithoutPosts()
        {
            var response = butterClient.ListCategories();
            Assert.IsNotNull(response);
            Assert.IsNull(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public async Task ListCategoriesAsync_ShouldReturnListOfCategoriesWithoutPosts()
        {
            var response = await butterClient.ListCategoriesAsync();
            Assert.IsNotNull(response);
            Assert.IsNull(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public void ListCategories_ShouldReturnListOfCategoriesWithPosts()
        {
            var response = butterClient.ListCategories(true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public async Task ListCategoriesAsync_ShouldReturnListOfCategoriesWithPosts()
        {
            var response = await butterClient.ListCategoriesAsync(true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);
        }
    }
}
