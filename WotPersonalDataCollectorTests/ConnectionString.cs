using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.AnyRoot;

namespace WotPersonalDataCollectorTests
{
    internal static class ConnectionString
    {
        public static string CosmosDbConnectionString(this BasicGenerator genRoot)
        {
            return
                "AccountEndpoint=https://XXXX/;AccountKey=xxxxxxxxx/xxxxxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyy==";
        }
    }
}
