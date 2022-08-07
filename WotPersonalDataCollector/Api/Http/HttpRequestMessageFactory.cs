using System.Net.Http;

namespace WotPersonalDataCollector.Api.Http
{
    internal class HttpRequestMessageFactory: IHttpRequestMessageFactory
    {
        private readonly IApiUrlFactory _apiUrlFactory;

        public HttpRequestMessageFactory(IApiUrlFactory apiUrlFactory)
        {
            _apiUrlFactory = apiUrlFactory;
        }
        public HttpRequestMessage Create(string apiUri)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _apiUrlFactory.Create(apiUri));
            requestMessage.Headers.Add("Accept", "application/json");
            return requestMessage;
        }
    }
}
