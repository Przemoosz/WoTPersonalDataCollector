using System.Net.Http;

namespace WotPersonalDataCollector.Api.Http;

internal interface IUserRequestMessageFactory
{
    HttpRequestMessage Create(string apiUri);
}