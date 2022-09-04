using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Utilities;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Builder;
using WotPersonalDataCollector.Workflow.Factory;


namespace WotPersonalDataCollector
{
    internal class WotPersonalDataCrawler
    {
        private readonly IWorkflowStepsFactory _workflowStepsFactory;
        private readonly IConfiguration _configuration;

        public WotPersonalDataCrawler(IWorkflowStepsFactory workflowStepsFactory, IConfiguration configuration)
        {
            _workflowStepsFactory = workflowStepsFactory;
            _configuration = configuration;
        }

        [FunctionName("WotPersonalDataCrawler")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var startingWorkflow = new WorkflowBuilder()
                .AddStep(_workflowStepsFactory.CreateUserInfoRequestObject())
                .AddStep(_workflowStepsFactory.CreateUserInfoApiUri())
                .AddStep(_workflowStepsFactory.CreateUserInfoHttpRequestMessage())
                .AddStep(_workflowStepsFactory.CreateSendRequestForUserId())
                .AddStep(_workflowStepsFactory.CreateDeserializeUserIdResponseMessage())
                .AddStep(_workflowStepsFactory.CreateUserPersonalDataRequestObject())
                .AddStep(_workflowStepsFactory.CreateUserPersonalDataApiUri())
                .AddStep(_workflowStepsFactory.CreateUserPersonalDataHttpRequestMessage())
                .AddStep(_workflowStepsFactory.CreateSendRequestForUserPersonalDataStep())
                .Build();
            await startingWorkflow.Execute(new WorkflowContext()
            {
                Logger = log, UserInfoApiUrl = _configuration.PlayersUri,
                UserPersonalDataApiUrl = _configuration.PersonalDataUri
            });
        }
    }
}
