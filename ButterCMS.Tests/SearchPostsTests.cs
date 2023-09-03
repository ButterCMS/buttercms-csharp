using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("SearchPosts")]
    public class SearchPostsTests
    {
        private ButterCMSClientWithMockedHttp butterClient;
        
        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void SearchPosts_ShouldReturnPosts()
        {
            var query = "api";
            var page = 1;
            var pageSize = 2;

            butterClient.MockSuccessfullSearchPostsResponse(page, pageSize, query: query);

            var posts = butterClient.SearchPosts(query: "api", page:1, pageSize:2);
            Assert.IsNotNull(posts);

            CollectionAssert.IsNotEmpty(posts.Data);

            var post = posts.Data.First();
            Assert.AreEqual(PostsMocks.Post.Slug, post.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Title); 
        }

        [Test]
        public async Task SearchPostsAsync_ShouldReturnPosts()
        {
            var query = "api";
            var page = 1;
            var pageSize = 2;

            butterClient.MockSuccessfullSearchPostsResponse(page, pageSize, query: query);

            var posts = await butterClient.SearchPostsAsync(query: "api", page: 1, pageSize: 2);
            Assert.IsNotNull(posts);

            CollectionAssert.IsNotEmpty(posts.Data);

            var post = posts.Data.First();
            Assert.AreEqual(PostsMocks.Post.Slug, post.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Title); 
        }

        [Test]
        public void SearchPosts_ShouldReturnNull()
        {
            var query = "FAKE SEARCH QUERY";
            var page = 1;
            var pageSize = 2;

            butterClient.MockSuccessfullEmptySearchPostsResponse(page, pageSize: pageSize, query: query);

            var posts = butterClient.SearchPosts(query: "FAKE SEARCH QUERY", page: 1, pageSize: 2);

            Console.WriteLine(JsonConvert.SerializeObject(posts));
            Assert.IsNull(posts);
        }

        [Test]
        public async Task SearchPostsAsync_ShouldReturnNull()
        {
            var query = "FAKE SEARCH QUERY";
            var page = 1;
            var pageSize = 2;

            butterClient.MockSuccessfullEmptySearchPostsResponse(page, pageSize: pageSize, query: query);

            var posts = await butterClient.SearchPostsAsync(query: "FAKE SEARCH QUERY", page: 1, pageSize: 2);
            Assert.IsNull(posts);
        }
    }

}
