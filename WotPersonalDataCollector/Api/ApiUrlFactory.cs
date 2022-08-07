using System.Text;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Api
{
    internal class ApiUrlFactory: IApiUrlFactory
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;

        public ApiUrlFactory(IUserInfoRequestObjectFactory userInfoRequestObjectFactory)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
        }

        public string Create(string baseUrl)
        {
            var requestObject = _userInfoRequestObjectFactory.Create();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(baseUrl);
            stringBuilder.Append("?");
            foreach (var property in requestObject.GetType().GetProperties())
            {
                stringBuilder.Append(property.Name);
                stringBuilder.Append("=");
                stringBuilder.Append(requestObject.GetType().GetProperty(property.Name)!.GetValue(requestObject, null));
                stringBuilder.Append("&");
            }
            return stringBuilder.ToString();
        }
    }
}
