using ButterCMS.Tests.Models;
using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class PagesMocks
    {
        public static things Fields = new things() { thing1 = "Bike", thing2 = "MTB" };

        public static Page<things> Page = new Page<things>()
        {
            Name = "Bike page",
            Slug = "bikes",
            Updated = new System.DateTime(),
            PageType = "BikeList",
            Fields = Fields
        };

        public static PagesResponse<things> PagesResponse = new PagesResponse<things>()
        {
            Data = new[] { Page },
            Meta = new PageMeta()
            {
                Count = 1,
                NextPage = null,
                PreviousPage = null
            }
        };

        public static void MockSuccessfullPagesResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/things/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", PagesResponse);
        }
    }
}
