using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.User
{
    internal class CrawlUserId
    {
        private readonly IHttpClientWrapperFactory _clientWrapperFactory;
        private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
 

        public CrawlUserId(IHttpClientWrapperFactory clientWrapperFactory, IHttpRequestMessageFactory httpRequestMessageFactory)
        {
            _clientWrapperFactory = clientWrapperFactory;
            _httpRequestMessageFactory = httpRequestMessageFactory;
        }

        public async Task<HttpResponseMessage> GetUserId()
        {
            using var client = _clientWrapperFactory.Create();
            var requestMessage = _httpRequestMessageFactory.Create("https://api.worldoftanks.eu/wot/account/list/");
            var result = await client.PostAsync(requestMessage);
            return result;
        }
    }
}
