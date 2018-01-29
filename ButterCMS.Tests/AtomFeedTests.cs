using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class AtomFeedTests
    {
        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void GetAtomFeedTest_ShouldReturnXMLDocument()
        {
            var xml = butterClient.GetAtomFeed();
            Assert.IsNotNull(xml);
        }

        [Test]
        public async Task GetAtomFeedAsyncTest_ShouldReturnXMLDocument()
        {
            var xml = await butterClient.GetAtomFeedAsync();
            Assert.IsNotNull(xml);
        }
    }
}
