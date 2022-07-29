using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotPersonalDataCollector
{
    internal class RequestObject: IRequestObject
    {
        public string application_id { get; set; }
    }

    internal interface IRequestObject
    {
        public string application_id { get; }
    }
}
