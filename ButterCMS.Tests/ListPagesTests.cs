using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
namespace ButterCMS.Tests
{
    [TestFixture]
    public class ListPagesTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void ListPages_ShouldReturnPages()
        {
            var dict = new Dictionary<string, string>()
            {
                {"fields.thing1", "1"},
            };
            var response = butterClient.ListPages<things>("things", dict);
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task ListPagesAsync_ShouldReturnPages()
        {
            var response = await butterClient.ListPagesAsync<things>("things");
            Assert.IsNotNull(response);
        }

        [Test]
        public void ListPages_NoResults_ShouldReturnNull()
        {
            var response =  butterClient.ListPages<things>("nothings");
            Assert.IsNull(response);
        }

        [Test]
        public async Task ListPagesAsync_NoResults_ShouldReturnNull()
        {
            var response = await butterClient.ListPagesAsync<things>("nothings");
            Assert.IsNull(response);
        }
    }
}
