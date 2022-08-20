using System.Net.Http;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.User.DTO;

namespace WotPersonalDataCollector.Api.User;

internal interface IDeserializeUserIdHttpResponse
{
    Task<UserIdData> Deserialize(HttpResponseMessage responseMessage);
}