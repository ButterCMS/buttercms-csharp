using NUnit.Framework;
using System.Configuration;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class ListPostsTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void ListPosts_ShouldReturnPosts()
        {
            var posts = butterClient.ListPosts();
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);
        }

        [Test]
        public void ListPosts_FullParams_ShouldReturnPosts()
        {   
            var posts = butterClient.ListPosts(page: 1, pageSize: 2, excludeBody: true, authorSlug: "api-test", categorySlug: "test-category", tagSlug: "test-tag");
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);
        }

        [Test]
        public void ListPosts_NoResults_ShouldReturnNull()
        {   
            var posts = butterClient.ListPosts(page: 2);
            Assert.IsNull(posts);
        }

        [Test]
        public async Task ListPostsAsync_ShouldReturnPosts()
        {   
            var posts = await butterClient.ListPostsAsync();
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);
        }

        [Test]
        public async Task ListPostsAsync_FullParams_ShouldReturnPosts()
        {   
            var posts = await butterClient.ListPostsAsync(page: 1, pageSize: 2, excludeBody: true, authorSlug: "api-test", categorySlug: "test-category", tagSlug: "test-tag");
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);
        }

        [Test]
        public async Task ListPostsAsync_NoResults_ShouldReturnNull()
        {   
            var posts = await butterClient.ListPostsAsync(page: 2, authorSlug: "not-an-author");
            Assert.IsNull(posts);
        }
    }
}
