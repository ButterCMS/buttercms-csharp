using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrieveAuthorTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void RetrieveAuthor_ShouldReturnAuthorWithoutPosts()
        {
            var response = butterClient.RetrieveAuthor("api-test");
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task RetrieveAuthorAsync_ShouldReturnAuthorWithoutPosts()
        {
            var response = await butterClient.RetrieveAuthorAsync("api-test");
            Assert.IsNotNull(response);
        }

        [Test]
        public void RetrieveAuthor_ShouldReturnAuthorsWithPosts()
        {
            var response = butterClient.RetrieveAuthor(authorSlug: "api-test", includeRecentPosts: true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.RecentPosts);
        }

        [Test]
        public async Task RetrieveAuthorAsync_ShouldReturnAuthorsWithPosts()
        {
            var response = await butterClient.RetrieveAuthorAsync(authorSlug: "api-test", includeRecentPosts: true);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.RecentPosts);
        }

        [Test]
        public void RetrieveAuthor_ShouldReturnNull()
        {
            var response = butterClient.RetrieveAuthor(authorSlug: "not-a-real-author");
            Assert.IsNull(response);
        }

        [Test]
        public async Task RetrieveAuthorAsync_ShouldReturnNull()
        {
            var response = await butterClient.RetrieveAuthorAsync(authorSlug: "not-a-real-author");
            Assert.IsNull(response);
        }
    }
}
