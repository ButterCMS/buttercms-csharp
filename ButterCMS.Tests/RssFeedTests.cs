using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RssFeedTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void GetRssFeedTest_ShouldReturnXMLDocument()
        {
            var xml = butterClient.GetRSSFeed();
            Assert.IsNotNull(xml);
        }

        [Test]
        public async Task GetRssFeedAsyncTest_ShouldReturnXMLDocument()
        {
            var xml = await butterClient.GetRSSFeedAsync();
            Assert.IsNotNull(xml);
        }
    }
}
