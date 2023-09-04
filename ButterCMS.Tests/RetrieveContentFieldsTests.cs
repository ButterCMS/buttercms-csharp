using ButterCMS.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using ButterCMS.Models;
using System;

namespace ButterCMS.Tests
{
    [TestFixture]
    [Category("RetrieveContentFields")]
    public class RetrieveContentFieldsTests
    {
        private ButterCMSClientWithMockedHttp butterClient;

        [SetUp]
        public void SetUp()
        {
            butterClient = Common.SetUpMockedButterClient();
        }

        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnTeamMembersHeadline() 
        {
            var keys = new string[] { "team_members[name=Elon]", "homepage_headline" };

            butterClient.MockSuccessfullContentFieldsJSONResponse(keys, ContentFieldsMocks.ContentFieldsResponse.Data);

            var teamMembersAndHeadline = butterClient.RetrieveContentFieldsJSON(keys);
            AssertContentFieldJSON(teamMembersAndHeadline, ContentFieldsMocks.ContentFieldsResponse.Data);
        }

        [Test]
        public void RetrieveContentFieldsJSON_ShouldReturnEmptyString() 
        {
            var keys = new string[] { "NOTAREALKEY" };

            butterClient.MockSuccessfullContentFieldsJSONResponse(keys, null);

            var teamMembersAndHeadline = butterClient.RetrieveContentFieldsJSON(keys);
            AssertContentFieldJSON(teamMembersAndHeadline, null);
        }

        [Test]
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnTeamMembersHeadline() 
        {
            var keys = new string[] { "team_members[name=Elon]", "homepage_headline" };

            butterClient.MockSuccessfullContentFieldsJSONResponse(keys, ContentFieldsMocks.ContentFieldsResponse.Data);

            var teamMembersAndHeadline = await butterClient.RetrieveContentFieldsJSONAsync(keys);
            AssertContentFieldJSON(teamMembersAndHeadline, ContentFieldsMocks.ContentFieldsResponse.Data);
        }

        [Test]
        public async Task RetrieveContentFieldsJSONAsync_ShouldReturnEmptyString() 
        {
            var keys = new string[] { "NOTAREALKEY" };

            butterClient.MockSuccessfullContentFieldsJSONResponse(keys, null);

            var teamMembersAndHeadline = await butterClient.RetrieveContentFieldsJSONAsync(keys);
            AssertContentFieldJSON(teamMembersAndHeadline, null);
        }

        private void AssertContentFieldJSON(string json, TeamMembersHeadline expected)
        {
            var teamMembersAndHeadline = JsonConvert.DeserializeObject<TeamMembersHeadline>(json);

            if (expected == null)
            {
                Assert.IsNull(teamMembersAndHeadline);
                return;
            }

            Assert.AreEqual(expected.homepage_headline, teamMembersAndHeadline.homepage_headline);
            Assert.AreEqual(expected.team_members[0].bio, teamMembersAndHeadline.team_members[0].bio);
        }

        [Test]
        public void RetrieveContentFields_ShouldReturnTeamMembersHeadline()
        {
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };

            butterClient.MockSuccessfullContentFieldsResponse(keys, ContentFieldsMocks.ContentFieldsResponse);

            var teamMembersAndHeadline = butterClient.RetrieveContentFields<TeamMembersHeadline>(keys);
            Assert.IsNotNull(teamMembersAndHeadline);
        }

        [Test]
        public async Task RetrieveContentFieldsAsync_ShouldReturnTeamMembersHeadline()
        {
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };

            butterClient.MockSuccessfullContentFieldsResponse(keys, ContentFieldsMocks.ContentFieldsResponse);

            var teamMembersAndHeadline = await butterClient.RetrieveContentFieldsAsync<TeamMembersHeadline>(keys);
            Assert.IsNotNull(teamMembersAndHeadline);
        }

        [Test]
        
        public void  RetrieveContentFields_ShouldThrowContentFieldObjectMismatchException()
        {
            var keys = new string[2] { "team_members[name=Elon]", "homepage_headline" };

            butterClient.MockSuccessfullContentFieldsResponse(keys, ContentFieldsMocks.ContentFieldsResponse);

            Assert.Throws<ContentFieldObjectMismatchException>(() => butterClient.RetrieveContentFields<string>(keys));
        }
    }
}
