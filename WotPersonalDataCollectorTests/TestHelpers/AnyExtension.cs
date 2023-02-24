namespace WotPersonalDataCollector.Tests.TestHelpers
{
    using TddXt.AnyExtensibility;

    internal static class AnyExtension
    {
        public static string CosmosDbConnectionString(this BasicGenerator genRoot)
        {
            return
                "AccountEndpoint=https://XXXX/;AccountKey=xxxxxxxxx/xxxxxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyy==";
        }
    }
}
