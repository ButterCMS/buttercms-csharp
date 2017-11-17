using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Configuration;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrievePageTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void ListPage_ShouldReturnPage()
        {
            var response = butterClient.RetrievePage<things>("things", "thingsthingsthings");
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task ListPageAsync_ShouldReturnPage()
        {
            var response = await butterClient.RetrievePageAsync<things>("things", "thingsthingsthings");
            Assert.IsNotNull(response);
        }

        [Test]
        public void ListPage_NoResults_ShouldReturnNull()
        {
            var response = butterClient.RetrievePage<things>("nothings", "nothings");
            Assert.IsNull(response);
        }

        [Test]
        public async Task ListPageAsync_NoResults_ShouldReturnNull()
        {
            var response = await butterClient.RetrievePageAsync<things>("nothings", "nothings");
            Assert.IsNull(response);
        }

    }
}
