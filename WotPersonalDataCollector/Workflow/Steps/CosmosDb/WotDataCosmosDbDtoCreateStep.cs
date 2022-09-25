using System.Threading.Tasks;
using WotPersonalDataCollector.CosmosDb.DTO;

namespace WotPersonalDataCollector.Workflow.Steps.CosmosDb
{
    internal sealed class WotDataCosmosDbDtoCreateStep: BaseStep
    {
        private readonly IWotDataCosmosDbDtoFactory _wotDataCosmosDbDtoFactory;

        public WotDataCosmosDbDtoCreateStep(IWotDataCosmosDbDtoFactory wotDataCosmosDbDtoFactory)
        {
            _wotDataCosmosDbDtoFactory = wotDataCosmosDbDtoFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            context.CosmosDbDto = _wotDataCosmosDbDtoFactory.Create(context.AccountDto, context.UserIdData);
        }
    }
}
