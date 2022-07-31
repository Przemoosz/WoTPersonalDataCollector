using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Api.Http.RequestObjects
{
    internal class UserInfoRequestObject: IRequestObject
    {
        public string application_id { get; init; }
        public string search { get; set; }
    }
}
