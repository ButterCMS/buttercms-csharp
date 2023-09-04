using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("AtomFeed")]
    public class AtomFeedTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void GetAtomFeedTest_ShouldReturnXMLDocument()
        {
            butterClient.MockSuccessfullAtomResponse();

            var xml = butterClient.GetAtomFeed();
            Assert.IsNotNull(xml);

            Assert.AreEqual("hello", xml.ChildNodes[1].Name);
        }

        [Test]
        public async Task GetAtomFeedAsyncTest_ShouldReturnXMLDocument()
        {
            butterClient.MockSuccessfullAtomResponse();

            var xml = await butterClient.GetAtomFeedAsync();
            Assert.IsNotNull(xml);

            Assert.AreEqual("hello", xml.ChildNodes[1].Name);
        }
    }
}
