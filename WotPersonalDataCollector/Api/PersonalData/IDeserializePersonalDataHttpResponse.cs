using System.Net.Http;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.PersonalData.Dto;

namespace WotPersonalDataCollector.Api.PersonalData;

internal interface IDeserializePersonalDataHttpResponse
{
    Task<WotAccountDto> Deserialize(HttpResponseMessage responseMessage, WotApiResponseContractResolver contractResolver);
}