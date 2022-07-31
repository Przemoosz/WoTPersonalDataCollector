using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.User.DTO;

namespace WotPersonalDataCollector.Api.User;

internal interface IDeserializeHttpResponse
{
    Task<UserData> Deserialize(ILogger logger);
}