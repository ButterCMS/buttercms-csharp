using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("ListAuthors")]
    public class ListAuthorsTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void ListAuthors_ShouldReturnListOfAuthorsWithoutPosts()
        {
            butterClient.MockSuccessfullAuthorsResponse();

            var response = butterClient.ListAuthors();
            Assert.IsNotNull(response);

            var author = response.FirstOrDefault();
            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);
            Assert.IsNull(author.RecentPosts);
        }

        [Test]
        public async Task ListAuthorsAsync_ShouldReturnListOfAuthorsWithoutPosts()
        {
            butterClient.MockSuccessfullAuthorsResponse();

            var response = await butterClient.ListAuthorsAsync();
            Assert.IsNotNull(response);

            var author = response.FirstOrDefault();
            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);
            Assert.IsNull(author.RecentPosts);
        }

        [Test]
        public void ListAuthors_ShouldReturnListOfAuthorsWithPosts()
        {
            butterClient.MockSuccessfullAuthorsResponseWithPosts();

            var response = butterClient.ListAuthors(true);
            Assert.IsNotNull(response);

            var author = response.FirstOrDefault();
            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);

            Assert.AreEqual(AuthorsMocks.AuthorWithPosts.RecentPosts.First().Slug, author.RecentPosts.First().Slug);
        }

        [Test]
        public async Task ListAuthorsAsync_ShouldReturnListOfAuthorsWithPosts()
        {
            butterClient.MockSuccessfullAuthorsResponseWithPosts();

            var response = await butterClient.ListAuthorsAsync(true);
            Assert.IsNotNull(response);

            var author = response.FirstOrDefault();
            Assert.AreEqual(AuthorsMocks.Author.FirstName, author.FirstName);
            Assert.AreEqual(AuthorsMocks.Author.LastName, author.LastName);
            Assert.IsNotEmpty(response.FirstOrDefault().RecentPosts);

            Assert.AreEqual(AuthorsMocks.AuthorWithPosts.RecentPosts.First().Slug, author.RecentPosts.First().Slug);
        }
    }
}
