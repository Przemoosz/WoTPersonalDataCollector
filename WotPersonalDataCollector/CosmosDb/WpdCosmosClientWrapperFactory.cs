using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.CosmosDb
{
    internal sealed class WpdCosmosClientWrapperFactory: IWpdCosmosClientWrapperFactory
    {
        private readonly IConfiguration _configuration;
        private static IWpdCosmosClientWrapper _cosmosClient = null;

        public WpdCosmosClientWrapperFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IWpdCosmosClientWrapper Create()
        {
            if (_cosmosClient is null)
            {
                _cosmosClient = new WpdCosmosClientWrapper(_configuration);
            }
            return _cosmosClient;
        }
    }
}
