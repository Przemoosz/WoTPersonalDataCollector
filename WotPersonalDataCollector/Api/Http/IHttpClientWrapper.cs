using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.Http;

internal interface IHttpClientWrapper: IDisposable
{
    Task PostAsync(HttpRequestMessage requestMessage);
}