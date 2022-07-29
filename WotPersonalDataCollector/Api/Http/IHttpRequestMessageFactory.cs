using System.Net.Http;

namespace WotPersonalDataCollector.Api.Http;

internal interface IHttpRequestMessageFactory
{
    HttpRequestMessage Create(string apiUri);
}