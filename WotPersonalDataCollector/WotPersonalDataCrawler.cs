using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.CosmosDb;
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
        private readonly IWpdCosmosClientWrapperFactory _cosmosClientWrapperFactory;
        private readonly ICosmosContainerService _cosmosContainerService;
        private bool _cosmosDbSetUpFinished = false;

        public WotPersonalDataCrawler(IWorkflowStepsFactory workflowStepsFactory, IConfiguration configuration,
             IWpdCosmosClientWrapperFactory cosmosClientWrapperFactory, ICosmosContainerService cosmosContainerService)
        {
            _workflowStepsFactory = workflowStepsFactory;
            _configuration = configuration;
            _cosmosClientWrapperFactory = cosmosClientWrapperFactory;
            _cosmosContainerService = cosmosContainerService;
        }

        [FunctionName("WotPersonalDataCrawler")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            if (_cosmosDbSetUpFinished)
            {
                log.LogInformation("Creating database");
                var databaseObject = await _cosmosClientWrapperFactory.Create().CreateDatabaseIfNotExistsAsync();
                log.LogInformation("Database created");
                log.LogInformation("Creating container");
                await _cosmosContainerService.Create(databaseObject);
                log.LogInformation("Container created");
                log.LogInformation("Finished setup execution");
                _cosmosDbSetUpFinished = true;
            }

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
                .AddStep(_workflowStepsFactory.CreateWotApiResponseContractResolverStep())
                .AddStep(_workflowStepsFactory.CreateDeserializePersonalDataHttpResponseStep())
                .Build();

            var context = new WorkflowContext()
            {
                Logger = log,
                UserInfoApiUrl = _configuration.PlayersUri,
                UserPersonalDataApiUrl = _configuration.PersonalDataUri
            };
            await startingWorkflow.Execute(context);
        }
    }
}
