using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WotPersonalDataCollector
{
    internal class HttpRequestMessageFactory: IHttpRequestMessageFactory
    {
        public HttpRequestMessage Create(IRequestObject requestObject, string apiUri)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUri);
            requestMessage.Headers.Add("Accept", "application/json");
            var serializedObject = JsonConvert.SerializeObject(requestObject);
            requestMessage.Content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            return requestMessage;
        }
    }

    internal interface IHttpRequestMessageFactory
    {
        HttpRequestMessage Create(IRequestObject requestObject, string apiUri)
    }
}
