using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class SitemapMocks
    {
        public static string Feed = @"<?xml version=""1.0"" encoding=""UTF-8""?><hello>World</hello>";

        public static XmlAPIResponse FeedResponse = new XmlAPIResponse()
        {
            Data = Feed,
        };

        public static void MockSuccessfullSitemapResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/feeds/sitemap/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", FeedResponse);
        }

        public static void MockSuccessfullRssResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/feeds/rss/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", FeedResponse);
        }
    }
}
