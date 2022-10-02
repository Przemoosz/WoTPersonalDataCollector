using System.Threading.Tasks;
using WotPersonalDataCollector.CosmosDb.DatabaseContext;
using WotPersonalDataCollector.CosmosDb.DTO;

namespace WotPersonalDataCollector.CosmosDb.Services
{
    internal sealed class CosmosDbService : ICosmosDbService
    {
        private readonly IWotContextWrapperFactory _wotContextWrapperFactory;

        public CosmosDbService(IWotContextWrapperFactory wotContextWrapperFactory)
        {
            _wotContextWrapperFactory = wotContextWrapperFactory;
        }

        public async Task SaveAsync(WotDataCosmosDbDto wotDataCosmosDbDto)
        {
            await using (var context = _wotContextWrapperFactory.Create())
            {
                await context.AddPersonalDataAsync(wotDataCosmosDbDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
