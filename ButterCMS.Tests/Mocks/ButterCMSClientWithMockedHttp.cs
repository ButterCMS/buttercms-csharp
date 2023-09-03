using RichardSzalay.MockHttp;
using Newtonsoft.Json;

namespace ButterCMS.Tests
{
    public class ButterCMSClientWithMockedHttp : ButterCMSClient
    {
        public static string MockedApiKey = "test-key";

        public readonly MockHttpMessageHandler mockHttpMessageHandler;

        private readonly JsonSerializerSettings serializerSettings;

        public ButterCMSClientWithMockedHttp(MockHttpMessageHandler mockHttpMessageHandler) : base(MockedApiKey, httpMessageHandler: mockHttpMessageHandler)
        {
            this.mockHttpMessageHandler = mockHttpMessageHandler;

            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new SnakeCaseContractResolver()
            };
        }

        public void MockSuccessfullJSONResponse<T>(string url, T response) {
            mockHttpMessageHandler
                .Expect(url)
                .Respond("application/json", JsonConvert.SerializeObject(response, serializerSettings));
        }
    }
}
