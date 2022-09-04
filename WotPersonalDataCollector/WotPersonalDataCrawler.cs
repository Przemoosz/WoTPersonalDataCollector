using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Builder;
using WotPersonalDataCollector.Workflow.Factory;


namespace WotPersonalDataCollector
{
    internal class WotPersonalDataCrawler
    {
        private readonly IWorkflowStepsFactory _workflowStepsFactory;

        public WotPersonalDataCrawler(IWorkflowStepsFactory workflowStepsFactory)
        {
            _workflowStepsFactory = workflowStepsFactory;
        }

        [FunctionName("WotPersonalDataCrawler")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var startingWorkflow = new WorkflowBuilder()
                .AddStep(_workflowStepsFactory.CreateUserInfoRequestObject())
                .AddStep(_workflowStepsFactory.CreateUserInfoApiUrl())
                .AddStep(_workflowStepsFactory.CreateUserInfoHttpRequestMessage())
                .AddStep(_workflowStepsFactory.CreateSendRequestForUserId())
                .AddStep(_workflowStepsFactory.CreateDeserializeUserIdResponseMessage())
                .AddStep(_workflowStepsFactory.CreateUserPersonalDataRequestObject())
                .Build();

            // TODO
            await startingWorkflow.Execute(new WorkflowContext()
            {
                Logger = log, UserInfoApiUrl = "https://api.worldoftanks.eu/wot/account/list/",
                UserPersonalDataApiUrl = "https://api.worldoftanks.eu/wot/account/info/"
            });

        }
    }
}
