using System.Net.Http;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.User;

internal interface ICrawlUserId
{
    Task<HttpResponseMessage> GetUserApiResponseAsync();
}