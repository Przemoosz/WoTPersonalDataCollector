using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Exceptions;

namespace WotPersonalDataCollector.Api.User
{
    internal class DeserializeHttpResponse: IDeserializeHttpResponse
    {
        private readonly IWotService _wotService;

        public DeserializeHttpResponse(IWotService wotService)
        {
            _wotService = wotService;
        }

        public async Task<UserData> Deserialize()
        {
            var response = await _wotService.GetUserIdApiResponseAsync(null);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Do not received 200 Ok from server instead received {response.StatusCode.ToString()}");
            }
            var deserializedData = JsonConvert.DeserializeObject<ResponseObject>(await response.Content.ReadAsStringAsync());
            if (deserializedData is null || deserializedData.GetType().GetProperties().Any(s => s.GetValue(deserializedData) is null))
            {
                throw new DeserializeJsonException("Can not deserialize data received from Wot API");
            }
            if (!deserializedData.status.Equals("ok"))
            {
                throw new WotApiResponseException("Wot Api returned error message!");
            }
            if (deserializedData.meta.count != 1)
            {
                throw new WotApiResponseException($"Received {deserializedData.meta.count} user, provide user id by yourself or check input WotUserName");
            }
            return deserializedData.data[0];
        }
    }
}
