using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Api
{
    internal class ApiUrlFactory: IApiUrlFactory
    {
        private readonly IRequestObjectFactory _requestObjectFactory;

        public ApiUrlFactory(IRequestObjectFactory requestObjectFactory)
        {
            _requestObjectFactory = requestObjectFactory;
        }

        public string Create(string baseUrl)
        {
            var requestObject = _requestObjectFactory.Create();
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
