using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.Api.Http.RequestObjects
{
    internal class UserPersonalDataRequestObjectFactory: IUserPersonalDataRequestObjectFactory
    {
        private readonly IConfiguration _configuration;

        public UserPersonalDataRequestObjectFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IRequestObject Create(UserIdData userIdData)
        {
            var requestObject = new UserPersonalDataRequestObject()
            {
                application_id = _configuration.ApplicationId,
                account_id = userIdData.AccountId.ToString()
            };
            return requestObject;
        }
    }
}
