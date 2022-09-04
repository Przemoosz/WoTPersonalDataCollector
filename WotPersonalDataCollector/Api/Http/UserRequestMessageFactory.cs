using System.Net.Http;

namespace WotPersonalDataCollector.Api.Http
{
    internal class UserRequestMessageFactory: IUserRequestMessageFactory
    {
        private readonly IApiUriFactory _apiUriFactory;

        public UserRequestMessageFactory(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }
        public HttpRequestMessage Create(string apiUri)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUri);
            requestMessage.Headers.Add("Accept", "application/json");
            return requestMessage;
        }
    }
}
