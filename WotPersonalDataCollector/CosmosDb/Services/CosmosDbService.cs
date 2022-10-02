using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.CosmosDb.DatabaseContext;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.Utilities;

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
                try
                {
                    await context.AddPersonalDataAsync(wotDataCosmosDbDto);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
