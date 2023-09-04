using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RetrievePost")]
    public class RetrievePostTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void RetrievePost_ShouldReturnPost()
        {
            var slug = PostsMocks.Post.Slug;

            butterClient.MockSuccessfullPostResponse(slug);

            var post = butterClient.RetrievePost(slug);
            Assert.IsNotNull(post);

            Assert.AreEqual(PostsMocks.Post.Slug, post.Data.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Data.Title); 
            Assert.AreEqual(PostsMocks.PostResponse.Meta.NextPost.Slug, post.Meta.NextPost.Slug);
        }

        [Test]
        public void RetrievePost_ShouldReturnNull()
        {
            var slug = "inavlid-slug-its-so-invalid-its-a-snail-not-a-slug";

            butterClient.MockSuccessfullNullPostResponse(slug);

            var post = butterClient.RetrievePost(slug);
            Assert.IsNull(post.Data);
        }

        [Test]
        public async Task RetrievePostAsync_ShouldReturnPost()
        {
            var slug = PostsMocks.Post.Slug;

            butterClient.MockSuccessfullPostResponse(slug);

            var post = await butterClient.RetrievePostAsync(slug);
            Assert.IsNotNull(post);

            Assert.AreEqual(PostsMocks.Post.Slug, post.Data.Slug); 
            Assert.AreEqual(PostsMocks.Post.Title, post.Data.Title); 
            Assert.AreEqual(PostsMocks.PostResponse.Meta.NextPost.Slug, post.Meta.NextPost.Slug);
        }
        [Test]
        public async Task RetrievePostAsync_ShouldReturnNull()
        {
            var slug = "inavlid-slug-its-so-invalid-its-a-snail-not-a-slug";

            butterClient.MockSuccessfullNullPostResponse(slug);

            var post = await butterClient.RetrievePostAsync(slug);
            Assert.IsNull(post.Data);
        }
    }
}
