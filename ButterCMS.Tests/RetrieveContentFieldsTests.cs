using ButterCMS.Tests.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ButterCMS.Tests
{
    [TestFixture]
    public class RetrieveContentFieldsTests
    {
        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnDictionaryString()
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
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnDictionaryString()
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

        [Test]
        public void RetrieveContentFields_ShouldReturnTeamMembersHeadline()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };
            var teamMembersAndHeadline = butterClient.RetrieveContentFields<TeamMembersHeadline>(keys);
            Assert.IsNotNull(teamMembersAndHeadline);
        }

        [Test]
        public async Task RetrieveContentFieldsAsync_ShouldReturnTeamMembersHeadline()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };
            var teamMembersAndHeadline = await butterClient.RetrieveContentFieldsAsync<TeamMembersHeadline>(keys);
            Assert.IsNotNull(teamMembersAndHeadline);
        }

        [Test]
        
        public void  RetrieveContentFields_ShouldThrowContentFieldObjectMismatchException()
        {
            var butterClient = new ButterCMSClient("321478403e868f0fc41f0115731f330ff720ce0b");
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };
            Assert.Throws<ContentFieldObjectMismatchException>(() => butterClient.RetrieveContentFields<string>(keys));
        }
    }
}
