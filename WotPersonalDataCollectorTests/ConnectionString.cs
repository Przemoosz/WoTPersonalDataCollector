using TddXt.AnyExtensibility;

namespace WotPersonalDataCollector.Tests
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
