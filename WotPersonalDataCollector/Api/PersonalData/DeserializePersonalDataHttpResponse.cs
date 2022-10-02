using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WotPersonalDataCollector.Api.PersonalData.Dto;
using WotPersonalDataCollector.Exceptions;

namespace WotPersonalDataCollector.Api.PersonalData
{
    internal sealed class DeserializePersonalDataHttpResponse: IDeserializePersonalDataHttpResponse
    {
        public async Task<WotAccountDto> Deserialize(HttpResponseMessage responseMessage, WotApiResponseContractResolver contractResolver)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver
            };
            WotAccountDto accountDto =
                JsonConvert.DeserializeObject<WotAccountDto>(await responseMessage.Content.ReadAsStringAsync(),
                    jsonSerializerSettings);
            if (accountDto is null || accountDto.GetType().GetProperties().Any(p => p.GetValue(accountDto) is null))
            {
                throw new DeserializeJsonException("Can not deserialize user personal data received from Wot Api!");
            }
            return accountDto;
        }
    }
}
