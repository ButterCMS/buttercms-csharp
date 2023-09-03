using ButterCMS.Models;

namespace ButterCMS.Tests
{
    public static class SitemapMocks
    {
        public static string Sitemap = @"<?xml version=""1.0"" encoding=""UTF-8""?><Hello>World</Hello>";

        public static XmlAPIResponse SitemapResponse = new XmlAPIResponse()
        {
            Data = Sitemap,
        };

        public static void MockSuccessfullSitemapResponse(this ButterCMSClientWithMockedHttp butterClient)
        {
            butterClient.MockSuccessfullJSONResponse($"https://api.buttercms.com/v2/feeds/sitemap/?auth_token={ButterCMSClientWithMockedHttp.MockedApiKey}", SitemapResponse);
        }
    }
}
