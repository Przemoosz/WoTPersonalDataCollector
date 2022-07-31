using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WotPersonalDataCollector.Api.Http.RequestObjects;

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
            // var serializedObject = JsonConvert.SerializeObject(_requestObject.Create());
            // requestMessage.Content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            return requestMessage;
        }
    }
}
