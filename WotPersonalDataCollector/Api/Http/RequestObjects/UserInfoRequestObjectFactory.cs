using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.Api.Http.RequestObjects
{
    internal class UserInfoRequestObjectFactory: IRequestObjectFactory
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
                search = _configuration.UserName
            };
            return requestObject;
        }
    }
}
