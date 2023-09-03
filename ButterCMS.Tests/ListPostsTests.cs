using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("ListPosts")]
    public class ListPostsTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void ListPosts_ShouldReturnPosts()
        {
            butterClient.MockSuccessfullPostsResponse();

            var posts = butterClient.ListPosts();
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);

            var post = posts.Data.First();
            Assert.AreEqual(PostsMocks.Post.Slug, post.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Title); 
        }

        [Test]
        public void ListPosts_FullParams_ShouldReturnPosts()
        {   
            var page = 1;
            var pageSize = 2;
            var excludeBody = true;
            var authorSlug = "test-author";
            var categorySlug = "test-category";
            var tagSlug = "test-tag";

            butterClient.MockSuccessfullPostsResponse(page, pageSize, excludeBody, authorSlug, categorySlug, tagSlug);

            var posts = butterClient.ListPosts(page: page, pageSize: pageSize, excludeBody: excludeBody, authorSlug: authorSlug, categorySlug: categorySlug, tagSlug: tagSlug);
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);

            var post = posts.Data.First();
            Assert.AreEqual(PostsMocks.Post.Slug, post.Slug);
            Assert.AreEqual(PostsMocks.Post.Title, post.Title);
        }

        [Test]
        public void ListPosts_NoResults_ShouldReturnNull()
        {   
            var page = 2;

            butterClient.MockSuccessfullEmptyPostsResponse(page);

            var posts = butterClient.ListPosts(page: page);
            Assert.AreEqual(posts.Meta.Count, 0);
            CollectionAssert.IsEmpty(posts.Data);
        }

        [Test]
        public async Task ListPostsAsync_ShouldReturnPosts()
        {   
            butterClient.MockSuccessfullPostsResponse();

            var posts = await butterClient.ListPostsAsync();
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);

            var post = posts.Data.First();
            Assert.AreEqual(PostsMocks.Post.Slug, post.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Title); 
        }

        [Test]
        public async Task ListPostsAsync_FullParams_ShouldReturnPosts()
        {   
            var page = 1;
            var pageSize = 2;
            var excludeBody = true;
            var authorSlug = "test-author";
            var categorySlug = "test-category";
            var tagSlug = "test-tag";

            butterClient.MockSuccessfullPostsResponse(page, pageSize, excludeBody, authorSlug, categorySlug, tagSlug);

            var posts = await butterClient.ListPostsAsync(page: page, pageSize: pageSize, excludeBody: excludeBody, authorSlug: authorSlug, categorySlug: categorySlug, tagSlug: tagSlug);
            Assert.IsNotNull(posts);
            CollectionAssert.IsNotEmpty(posts.Data);

            var post = posts.Data.First();
            Assert.AreEqual(PostsMocks.Post.Slug, post.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Title); 
        }

        [Test]
        public async Task ListPostsAsync_NoResults_ShouldReturnNull()
        {   
            var page = 2;
            var authorSlug = "not-an-author";

            butterClient.MockSuccessfullEmptyPostsResponse(page, authorSlug);

            var posts = await butterClient.ListPostsAsync(page: page, authorSlug: authorSlug);

            Assert.AreEqual(posts.Meta.Count, 0);
            CollectionAssert.IsEmpty(posts.Data);
        }
    }
}
