using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.Http
{
    internal class UserDataRequestMessageFactory: IUserDataRequestMessageFactory
    {
        private readonly IApiUriFactory _apiUriFactory;

        public UserDataRequestMessageFactory(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }

        public HttpRequestMessage Create()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IUserDataRequestMessageFactory
    {
    }
}
