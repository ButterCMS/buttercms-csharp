using ButterCMS.Tests.Models;
using ButterCMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButterCMS.Tests
{
    public static class PagesMocks
    {
        public static Things Fields = new Things() { Thing1 = "Bike", Thing2 = "MTB" };

        public const string PageType = "BikeList";

        public static Page<Things> Page = new Page<Things>()
        {
            Name = "Bike page",
            Slug = "bikes",
            Updated = new System.DateTime(),
            PageType = PageType,
            Fields = Fields
        };

        public static PagesResponse<Things> PagesResponse = new PagesResponse<Things>()
        {
            Data = new[] { Page },
            Meta = new PageMeta()
            {
                Count = 1,
                NextPage = null,
                PreviousPage = null
            }
        };

        public static PageResponse<Things> PageResponse = new PageResponse<Things>()
        {
            Data = Page,
        };

        public static void MockSuccessfullPagesResponse(this ButterCMSClientWithMockedHttp butterClient, string pageType = PageType, Dictionary<string, string> parameters = null)
        {
            var parametersQuery = parameters != null ? $"&{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))}" : string.Empty;

            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/{pageType}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}{parametersQuery}", PagesResponse, new JsonSerializerSettings());
        }

        public static void MockSuccessfullPageResponse(this ButterCMSClientWithMockedHttp butterClient, string slug, string pageType = PageType) 
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/{pageType}/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", PageResponse, new JsonSerializerSettings());
        }

        public static void MockSuccessfullNullPageResponse(this ButterCMSClientWithMockedHttp butterClient, string slug, string pageType = PageType)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/pages/{pageType}/{slug}/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", new PageResponse<Things>() { Data = null });
        }
    }
}
