using System.Net.Http;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollector.Api.Services
{
    internal class WotService: IWotService
    {
        private readonly IHttpClientWrapperFactory _clientWrapperFactory;

        public WotService(IHttpClientWrapperFactory clientWrapperFactory)
        {
            _clientWrapperFactory = clientWrapperFactory;
        }

        public async Task<HttpResponseMessage> GetUserIdApiResponseAsync(HttpRequestMessage requestMessage)
        {
            using var client = _clientWrapperFactory.Create();
            var result = await client.PostAsync(requestMessage);
            result.EnsureSuccessStatusCode();
            return result;
        }
    }
}
