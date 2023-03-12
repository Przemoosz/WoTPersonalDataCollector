namespace WotPersonalDataCollector.TestHelpers.Extensions
{
	using TddXt.AnyExtensibility;

	public static class AnyExtension
    {
        public static string CosmosDbConnectionString(this BasicGenerator genRoot)
        {
            return
                "AccountEndpoint=https://XXXX/;AccountKey=xxxxxxxxx/xxxxxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyyxxxxyyyy==";
        }
    }
}
