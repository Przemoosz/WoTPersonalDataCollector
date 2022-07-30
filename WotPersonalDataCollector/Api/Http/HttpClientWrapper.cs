using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.Http
{
    internal class HttpClientWrapper:IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(4);
        }

        public async Task PostAsync(HttpRequestMessage requestMessage)
        {
            if (requestMessage is null)
            {
                throw new ArgumentNullException(nameof(requestMessage));
            }
            await _httpClient.SendAsync(requestMessage);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
