using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.CosmosDb.Services;

namespace WotPersonalDataCollector.Workflow.Steps.CosmosDb
{
    internal class SaveDataToCosmosDatabaseStep: BaseStep
    {
        private readonly ICosmosDbService _cosmosDbService;
        private bool _saved = true;

        public SaveDataToCosmosDatabaseStep(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                await _cosmosDbService.SaveAsync(context.CosmosDbDto);
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred when saving data to cosmosDb. Message: {exception.Message}\n At: {exception.StackTrace}");
                _saved = false;
                context.UnexpectedException = true;
            }
        }

        public override bool SuccessfulStatus() => _saved;
    }
}
