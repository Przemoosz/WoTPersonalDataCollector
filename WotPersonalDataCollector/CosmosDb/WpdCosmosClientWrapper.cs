namespace WotPersonalDataCollector.CosmosDb
{
	using System.Threading.Tasks;
	using Microsoft.Azure.Cosmos;
	using Utilities;

	internal sealed class WpdCosmosClientWrapper: IWpdCosmosClientWrapper
    {
        private readonly IConfiguration _configuration;
        private static CosmosClient _cosmosClient;

        public WpdCosmosClientWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
            if (_cosmosClient is null)
            {
                _cosmosClient = new CosmosClient(_configuration.CosmosConnectionString);
            }
        }

        public void Dispose()
        {
            _cosmosClient.Dispose();
        }

        public async Task<Database> CreateDatabaseIfNotExistsAsync()
        {
            ThroughputProperties throughputProperties = ThroughputProperties.CreateManualThroughput(_configuration.DatabaseThroughput);
            return await _cosmosClient.CreateDatabaseIfNotExistsAsync(_configuration.CosmosDbName, throughputProperties);
        }
    }
}
