using System;
using System.Net.Http;

namespace WotPersonalDataCollector.Api.Http
{
    internal class UserPersonalDataRequestMessageFactory: IUserPersonalDataRequestMessageFactory
    {
        private readonly IApiUriFactory _apiUriFactory;

        public UserPersonalDataRequestMessageFactory(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }

        public HttpRequestMessage Create()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IUserPersonalDataRequestMessageFactory
    {
    }
}
