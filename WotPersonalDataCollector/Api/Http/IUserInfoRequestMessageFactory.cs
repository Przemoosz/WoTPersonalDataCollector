using System.Net.Http;

namespace WotPersonalDataCollector.Api.Http;

internal interface IUserInfoRequestMessageFactory
{
    HttpRequestMessage Create(string apiUri);
}