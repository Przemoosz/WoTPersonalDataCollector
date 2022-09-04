using System.Text;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Api
{
    internal class ApiUriFactory: IApiUriFactory
    {
        public string Create(string baseUrl, IRequestObject requestObject)
        {
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
