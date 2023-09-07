using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RssFeed")]
    public class RssFeedTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void GetRssFeedTest_ShouldReturnXMLDocument()
        {
            butterClient.MockSuccessfullRssResponse();

            var xml = butterClient.GetRSSFeed();
            Assert.IsNotNull(xml);

            Assert.AreEqual("hello", xml.ChildNodes[1].Name);
        }

        [Test]
        public async Task GetRssFeedAsyncTest_ShouldReturnXMLDocument()
        {
            butterClient.MockSuccessfullRssResponse();

            var xml = await butterClient.GetRSSFeedAsync();
            Assert.IsNotNull(xml);

            Assert.AreEqual("hello", xml.ChildNodes[1].Name);
        }
    }
}
