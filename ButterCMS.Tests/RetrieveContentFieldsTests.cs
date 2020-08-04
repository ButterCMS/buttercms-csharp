using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrieveContentFieldsTests
    {

        private ButterCMSClient butterClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpButterClient();
        }

        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnDictionaryString()
        {
            var keys = new string[] { "hotproducts" };
            var contentFields = butterClient.RetrieveContentFieldsJSON(keys);
            Assert.IsNotNull(contentFields);
        }

        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnEmptyString()
        {
            var keys = new string[] { "NOTAREALKEY" };
            var actual = butterClient.RetrieveContentFieldsJSON(keys);
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnDictionaryString()
        {
            var keys = new string[] { "hotproducts" };
            var contentFields = await butterClient.RetrieveContentFieldsJSONAsync(keys);
            Assert.IsNotNull(contentFields);
        }

        [Test]
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnEmptyString()
        {
            var keys = new string[] { "NOTAREALKEY" };
            var actual = await butterClient.RetrieveContentFieldsJSONAsync(keys);
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RetrieveContentFields_ShouldReturnHotProducts()
        {
            var keys = new string[] { "hotproducts" };
            var hotProducts = butterClient.RetrieveContentFields<HotProductsResponse>(keys);
            Assert.IsNotNull(hotProducts);
        }

        [Test]
        public async Task RetrieveContentFieldsAsync_ShouldReturnHotProducts()
        {
            var keys = new string[] { "hotproducts" };
            var hotProducts = await butterClient.RetrieveContentFieldsAsync<HotProductsResponse>(keys);
            Assert.IsNotNull(hotProducts);
        }

        [Test]

        public void RetrieveContentFields_ShouldThrowContentFieldObjectMismatchException()
        {
            var keys = new string[] { "hotproducts" };
            Assert.Throws<ContentFieldObjectMismatchException>(() => butterClient.RetrieveContentFields<string>(keys));
        }
    }
}
