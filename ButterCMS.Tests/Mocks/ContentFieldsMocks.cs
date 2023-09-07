using Newtonsoft.Json;
using ButterCMS.Models;
using ButterCMS.Tests.Models;

namespace ButterCMS.Tests
{
    public static class ContentFieldsMocks
    {
        public static ContentResponse<TeamMembersHeadline> ContentFieldsResponse = new ContentResponse<TeamMembersHeadline>()
        {
            Data = new TeamMembersHeadline()
            {
                homepage_headline = "ButterCMS C# SDK",
                team_members = new[]
                {
                    new Team_Members()
                    {
                        name = "Elon",
                        bio = "CEO"
                    },
                    new Team_Members()
                    {
                        name = "Steve",
                        bio = "CTO"
                    }
                }
            }
        };

        public const string ContentFieldsJSON = "{\"data\":{\"team_members\":[{\"bio\":\"CEO\",\"name\":\"Elon\"},{\"bio\":\"CTO\",\"name\":\"Steve\"}],\"homepage_headline\":\"ButterCMS C# SDK\",}}";

        public static void MockSuccessfullContentFieldsJSONResponse(this ButterCMSClientWithMockedHttp butterClient, string[] keys, TeamMembersHeadline response)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/content/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&keys={string.Join(",", keys)}", response);
        }

        public static void MockSuccessfullContentFieldsResponse(this ButterCMSClientWithMockedHttp butterClient, string[] keys, ContentResponse<TeamMembersHeadline> response)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/content/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}&keys={string.Join(",", keys)}", response);
        }
    }
}
