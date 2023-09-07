using RichardSzalay.MockHttp;
using Newtonsoft.Json;

namespace ButterCMS.Tests
{
    public class ButterCMSClientWithMockedHttp : ButterCMSClient
    {
        public static string MockedApiKey = "test-key";

        public readonly MockHttpMessageHandler mockHttpMessageHandler;

        private readonly JsonSerializerSettings _serializerSettings;

        public ButterCMSClientWithMockedHttp(MockHttpMessageHandler mockHttpMessageHandler) : base(MockedApiKey, httpMessageHandler: mockHttpMessageHandler)
        {
            this.mockHttpMessageHandler = mockHttpMessageHandler;

            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new SnakeCaseContractResolver()
            };
        }

        public void MockSuccessfullJSONResponse<T>(string url, T response, JsonSerializerSettings serializerSettings = null) where T : class 
        {
            mockHttpMessageHandler
                .Expect(url)
                .Respond("application/json", JsonConvert.SerializeObject(response, serializerSettings ?? _serializerSettings));
        }
    }
}
