using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrieveContentFieldsTests
    {
        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnDictionaryStringDynamic()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[2] {"team_members[name=Elon]", "homepage_headline"};
            var contentFields = butterClient.RetrieveContentFieldsJSON(keys);
            Assert.IsNotNull(contentFields);
        }

        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnEmptyString()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[1] { "NOTAREALKEY" };
            var actual = butterClient.RetrieveContentFieldsJSON(keys);
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnDictionaryStringDynamic()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };
            var contentFields = await butterClient.RetrieveContentFieldsJSONAsync(keys);
            Assert.IsNotNull(contentFields);
        }

        [Test]
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnEmptyString()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[1] { "NOTAREALKEY" };
            var actual = await butterClient.RetrieveContentFieldsJSONAsync(keys);
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }
    }
}
