using RichardSzalay.MockHttp;
using Newtonsoft.Json;

namespace ButterCMS.Tests
{
    public class ButterCMSClientWithMockedHttp : ButterCMSClient
    {
        public static string MockedApiKey = "test-key";

        public readonly MockHttpMessageHandler mockHttpMessageHandler;

        public ButterCMSClientWithMockedHttp(MockHttpMessageHandler mockHttpMessageHandler) : base(MockedApiKey, httpMessageHandler: mockHttpMessageHandler)
        {
            this.mockHttpMessageHandler = mockHttpMessageHandler;
        }

        public void MockSuccessfullJSONResponse<T>(string url, T reponse) {
            mockHttpMessageHandler
                .When(url)
                .Respond("application/json", JsonConvert.SerializeObject(reponse));
        }
    }
}
