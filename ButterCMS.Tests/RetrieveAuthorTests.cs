using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RetrieveAuthor")]
    public class RetrieveAuthorTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void RetrieveAuthor_ShouldReturnAuthorWithoutPosts()
        {
            butterClient.MockSuccessfullAuthorResponse(AuthorsMocks.Author.Slug);

            var author = butterClient.RetrieveAuthor(AuthorsMocks.Author.Slug);
            Assert.IsNotNull(author);

            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);
            Assert.IsNull(author.RecentPosts);
        }

        [Test]
        public async Task RetrieveAuthorAsync_ShouldReturnAuthorWithoutPosts()
        {
            butterClient.MockSuccessfullAuthorResponse(AuthorsMocks.Author.Slug);

            var author = await butterClient.RetrieveAuthorAsync(AuthorsMocks.Author.Slug);
            Assert.IsNotNull(author);

            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);
            Assert.IsNull(author.RecentPosts);
        }

        [Test]
        public void RetrieveAuthor_ShouldReturnAuthorsWithPosts()
        {
            butterClient.MockSuccessfullAuthorResponseWithPosts(AuthorsMocks.AuthorWithPosts.Slug);

            var author = butterClient.RetrieveAuthor(authorSlug: AuthorsMocks.Author.Slug, includeRecentPosts: true);
            Assert.IsNotNull(author);

            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);

            Assert.IsNotEmpty(author.RecentPosts);
            Assert.AreEqual(AuthorsMocks.AuthorWithPosts.RecentPosts.First().Slug, author.RecentPosts.First().Slug);
        }

        [Test]
        public async Task RetrieveAuthorAsync_ShouldReturnAuthorsWithPosts()
        {
            butterClient.MockSuccessfullAuthorResponseWithPosts(AuthorsMocks.AuthorWithPosts.Slug);

            var author = await butterClient.RetrieveAuthorAsync(authorSlug: AuthorsMocks.Author.Slug, includeRecentPosts: true);
            Assert.IsNotNull(author);

            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);

            Assert.IsNotEmpty(author.RecentPosts);
            Assert.AreEqual(AuthorsMocks.AuthorWithPosts.RecentPosts.First().Slug, author.RecentPosts.First().Slug);
        }

        [Test]
        public void RetrieveAuthor_ShouldReturnNull()
        {
            var slug = "not-a-real-author";
            butterClient.MockSuccessfullNullAuthorResponse(slug);

            var response = butterClient.RetrieveAuthor(authorSlug: slug);
            Assert.IsNull(response);
        }

        [Test]
        public async Task RetrieveAuthorAsync_ShouldReturnNull()
        {
            var slug = "not-a-real-author";
            butterClient.MockSuccessfullNullAuthorResponse(slug);

            var response = await butterClient.RetrieveAuthorAsync(authorSlug: slug);
            Assert.IsNull(response);
        }
    }
}
