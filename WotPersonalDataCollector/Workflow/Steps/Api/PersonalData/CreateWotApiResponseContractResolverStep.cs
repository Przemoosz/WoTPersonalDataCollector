using System.Threading.Tasks;
using WotPersonalDataCollector.Api.PersonalData;

namespace WotPersonalDataCollector.Workflow.Steps.Api.PersonalData
{
    internal sealed class CreateWotApiResponseContractResolverStep: BaseStep
    {
        public override Task ExecuteInner(WorkflowContext context)
        {
            context.ContractResolver = new WotApiResponseContractResolver(context.UserIdData.AccountId.ToString());
            return Task.CompletedTask;
        }
    }
}
