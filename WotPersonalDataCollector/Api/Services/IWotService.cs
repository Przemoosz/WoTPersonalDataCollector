using System.Net.Http;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.Services;

internal interface IWotService
{
    Task<HttpResponseMessage> GetUserIdApiResponseAsync(HttpRequestMessage requestMessage);
}