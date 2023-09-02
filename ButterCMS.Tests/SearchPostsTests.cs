using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("SearchPosts")]
    public class SearchPostsTests
    {
        private ButterCMSClient butterClient;
        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void SearchPosts_ShouldReturnPosts()
        {
            var posts = butterClient.SearchPosts(query: "api", page:1, pageSize:2);
            Assert.IsNotNull(posts);
        }

        [Test]
        public async Task SearchPostsAsync_ShouldReturnPosts()
        {
            var posts = await butterClient.SearchPostsAsync(query: "api", page: 1, pageSize: 2);
            Assert.IsNotNull(posts);
        }

        [Test]
        public void SearchPosts_ShouldReturnNull()
        {
            var posts = butterClient.SearchPosts(query: "FAKE SEARCH QUERY", page: 1, pageSize: 2);
            Assert.IsNull(posts);
        }

        [Test]
        public async Task SearchPostsAsync_ShouldReturnNull()
        {
            var posts = await butterClient.SearchPostsAsync(query: "FAKE SEARCH QUERY", page: 1, pageSize: 2);
            Assert.IsNull(posts);
        }
    }

}
