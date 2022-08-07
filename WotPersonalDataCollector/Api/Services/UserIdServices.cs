using System.Net.Http;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollector.Api.Services
{
    internal class UserIdServices: IUserIdServices
    {
        private readonly IHttpClientWrapperFactory _clientWrapperFactory;
        private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
 

        public UserIdServices(IHttpClientWrapperFactory clientWrapperFactory, IHttpRequestMessageFactory httpRequestMessageFactory)
        {
            _clientWrapperFactory = clientWrapperFactory;
            _httpRequestMessageFactory = httpRequestMessageFactory;
        }

        public async Task<HttpResponseMessage> GetUserApiResponseAsync()
        {
            using var client = _clientWrapperFactory.Create();
            var requestMessage = _httpRequestMessageFactory.Create("https://api.worldoftanks.eu/wot/account/list/");
            var result = await client.PostAsync(requestMessage);
            return result;
        }
    }
}
