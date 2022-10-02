using System.Threading.Tasks;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.CosmosDb.DatabaseContext
{
    internal sealed class WotContextWrapper: IWotContextWrapper
    {
        private readonly IConfiguration _configuration;
        private readonly WotContext _wotContext;

        public WotContextWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _wotContext = new WotContext(_configuration);
        }

        public async Task AddPersonalDataAsync(WotDataCosmosDbDto wotDataCosmosDbDto)
        {
            await _wotContext.PersonalData.AddAsync(wotDataCosmosDbDto);
        }

        public async Task SaveChangesAsync()
        {
            await _wotContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _wotContext.DisposeAsync();
        }
    }
}
