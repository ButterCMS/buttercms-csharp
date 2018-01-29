using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class ListAuthorsTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void ListAuthors_ShouldReturnListOfAuthorsWithoutPosts()
        {
            var response = butterClient.ListAuthors();
            Assert.IsNotNull(response);
            Assert.IsNull(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public async Task ListAuthorsAsync_ShouldReturnListOfAuthorsWithoutPosts()
        {
            var response = await butterClient.ListAuthorsAsync();
            Assert.IsNotNull(response);
            Assert.IsNull(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public void ListAuthors_ShouldReturnListOfAuthorsWithPosts()
        {
            var response = butterClient.ListAuthors(true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);
        }

        [Test]
        public async Task ListAuthorsAsync_ShouldReturnListOfAuthorsWithPosts()
        {
            var response = await butterClient.ListAuthorsAsync(true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);
        }
    }
}
