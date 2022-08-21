using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Exceptions;

namespace WotPersonalDataCollector.Api.User
{
    internal class DeserializeUserIdHttpResponse: IDeserializeUserIdHttpResponse
    {
        public async Task<UserIdData> Deserialize(HttpResponseMessage responseMessage)
        {
            var deserializedData = JsonConvert.DeserializeObject<ResponseObject>(await responseMessage.Content.ReadAsStringAsync());
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
                throw new MoreThanOneUserException($"Received {deserializedData.meta.count} user, provide user id by yourself or check input WotUserName");
            }
            return deserializedData.data[0];
        }
    }
}
