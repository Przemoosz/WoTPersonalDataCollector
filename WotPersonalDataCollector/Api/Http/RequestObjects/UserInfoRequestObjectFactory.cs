using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.Api.Http.RequestObjects
{
    internal class UserInfoRequestObjectFactory: IUserInfoRequestObjectFactory
    {
        private readonly IConfiguration _configuration;

        public UserInfoRequestObjectFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IRequestObject Create()
        {
            var requestObject = new UserInfoRequestObject()
            {
                application_id = _configuration.ApplicationId,
            };
            if (_configuration.TryGetUserName(out string userName))
            {
                requestObject.search = userName;
            }
            else
            {
                requestObject.search = "";
            }
            return requestObject;
        }
    }
}
