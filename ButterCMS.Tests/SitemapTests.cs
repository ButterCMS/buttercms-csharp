using NUnit.Framework;
using System.Configuration;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class SitemapTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = new ButterCMSClient(ConfigurationManager.AppSettings["ButterAuthToken"]);
        }

        [Test]
        public void GetSitemapTest_ShouldReturnXMLDocument()
        {
            var xml = butterClient.GetSitemap();
            Assert.IsNotNull(xml);
        }

        [Test]
        public async Task GetSitemapAsyncTest_ShouldReturnXMLDocument()
        {
            var xml = await butterClient.GetSitemapAsync();
            Assert.IsNotNull(xml);
        }

        [Test]
        public void GetSitemapTest_ShouldThrowInvalidKeyException()
        {
            var invalidButterClient = new ButterCMSClient("I'm not a valid key");
            Assert.Throws<InvalidKeyException>(() => invalidButterClient.GetSitemap());
        }
    }
}
