using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RetrievePage")]
    public class RetrievePageTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void RetrievePage_ShouldReturnPage()
        {
            var response = butterClient.RetrievePage<things>("things", "thingsthingsthings");
            Assert.IsNotNull(response.Data.Fields.thing1);
            Assert.IsNotNull(response.Data.Name);
            Assert.IsNotNull(response.Data.PageType);
            Assert.IsNotNull(response.Data.Updated);
        }

        [Test]
        public async Task RetrievePageAsync_ShouldReturnPage()
        {
            var response = await butterClient.RetrievePageAsync<things>("things", "thingsthingsthings");
            Assert.IsNotNull(response);
        }

        [Test]
        public void RetrievePage_NoResults_ShouldReturnNull()
        {
            var response = butterClient.RetrievePage<things>("nothings", "nothings");
            Assert.IsNull(response);
        }

        [Test]
        public async Task RetrievePageAsync_NoResults_ShouldReturnNull()
        {
            var response = await butterClient.RetrievePageAsync<things>("nothings", "nothings");
            Assert.IsNull(response);
        }
    }
}
