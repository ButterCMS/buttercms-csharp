using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace ButterCMS.Tests
{
    public static class Common
    {
        private static IConfiguration Configuration { get; set; }
        public static ButterCMSClient SetUpButterClient()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return new ButterCMSClient(Configuration["apiKey"]);
        }

        public static ButterCMSClientWithMockedHttp SetUpMockedButterClient() => new ButterCMSClientWithMockedHttp(new MockHttpMessageHandler());
    }
}
