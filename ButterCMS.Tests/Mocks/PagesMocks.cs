using ButterCMS.Tests.Models;
using ButterCMS.Models;
using Newtonsoft.Json;

namespace ButterCMS.Tests
{
    public static class PagesMocks
    {
        public static things Fields = new things() { thing1 = "Bike", thing2 = "MTB" };

        public const string PageType = "BikeList";

        public static Page<things> Page = new Page<things>()
        {
            Name = "Bike page",
            Slug = "bikes",
            Updated = new System.DateTime(),
            PageType = PageType,
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

        public static PageResponse<things> PageResponse = new PageResponse<things>()
        {
            Data = Page,
        };

        public static void MockSuccessfullPagesResponse(this ButterCMSClientWithMockedHttp butterClient, string pageType = PageType)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/${pageType}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", PagesResponse);
        }

        public static void MockSuccessfullPageResponse(this ButterCMSClientWithMockedHttp butterClient, string slug, string pageType = PageType) 
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/{pageType}/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", PageResponse, new JsonSerializerSettings());
        }

        public static void MockSuccessfullNullPageResponse(this ButterCMSClientWithMockedHttp butterClient, string slug, string pageType = PageType)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/{pageType}/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", new PageResponse<things>() { Data = null });
        }
    }
}
