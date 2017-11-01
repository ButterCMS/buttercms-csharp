using NUnit.Framework;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class ListTagsTests
    {

        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void ListTags_ShouldReturnListOfTagsWithoutPosts()
        {
            var response = butterClient.ListTags();
            Assert.IsNotNull(response);
            Assert.IsNull(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public async Task ListTagsAsync_ShouldReturnListOfTagsWithoutPosts()
        {
            var response = await butterClient.ListTagsAsync();
            Assert.IsNotNull(response);
            Assert.IsNull(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public void ListTags_ShouldReturnListOfTagsWithPosts()
        {
            var response = butterClient.ListTags(true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public async Task ListTagsAsync_ShouldReturnListOfTagsWithPosts()
        {
            var response = await butterClient.ListTagsAsync(true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);
        }


    }
}
