using System.Net.Http;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.Services;

internal interface IUserIdServices
{
    Task<HttpResponseMessage> GetUserApiResponseAsync();
}