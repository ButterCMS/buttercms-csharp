using NUnit.Framework;
using System.Configuration;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrievePostTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void RetrievePost_ShouldReturnPost()
        {
            var post = butterClient.RetrievePost("this-is-a-blog-post");
            Assert.IsNotNull(post);
        }

        [Test]
        public void RetrievePost_ShouldReturnNull()
        {
            var post = butterClient.RetrievePost("inavlid-slug-its-so-invalid-its-a-snail-not-a-slug");
            Assert.IsNull(post);
        }

        [Test]
        public async Task RetrievePostAsync_ShouldReturnPost()
        {
            var post = await butterClient.RetrievePostAsync("this-is-a-blog-post");
            Assert.IsNotNull(post);
        }
        [Test]
        public async Task RetrievePostAsync_ShouldReturnNull()
        {
            var post = await butterClient.RetrievePostAsync("inavlid-slug-its-so-invalid-its-a-snail-not-a-slug");
            Assert.IsNull(post);
        }
    }
}
