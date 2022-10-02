using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.CosmosDb.DatabaseContext
{
    internal sealed class WotContextWrapperFactory: IWotContextWrapperFactory
    {
        private readonly IConfiguration _configuration;

        public WotContextWrapperFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WotContextWrapper Create()
        {
            return new WotContextWrapper(_configuration);
        }
    }
}
