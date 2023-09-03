using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("Sitemap")]
    public class SitemapTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void GetSitemapTest_ShouldReturnXMLDocument()
        {
            butterClient.MockSuccessfullSitemapResponse();

            var xml = butterClient.GetSitemap();
            Assert.IsNotNull(xml);

            Assert.AreEqual("hello", xml.ChildNodes[1].Name);
        }

        [Test]
        public async Task GetSitemapAsyncTest_ShouldReturnXMLDocument()
        {
            butterClient.MockSuccessfullSitemapResponse();

            var xml = await butterClient.GetSitemapAsync();
            Assert.IsNotNull(xml);

            Assert.AreEqual("hello", xml.ChildNodes[1].Name);
        }

        [Test]
        public void GetSitemapTest_ShouldThrowInvalidKeyException()
        {
            var invalidButterClient = new ButterCMSClient("I'm not a valid key");
            Assert.Throws<InvalidKeyException>(() => invalidButterClient.GetSitemap());
        }
    }
}
