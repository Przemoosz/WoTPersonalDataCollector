using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Builder;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;


namespace WotPersonalDataCollector
{
    internal class WotPersonalDataCrawler
    {
        private readonly IUserIdServices _userIdServices;
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;

        public WotPersonalDataCrawler(IUserIdServices userIdServices, IUserInfoRequestObjectFactory userInfoRequestObjectFactory)
        {
            _userIdServices = userIdServices;
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
        }

        [FunctionName("WotPersonalDataCrawler")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var startingWorkflow = new WorkflowBuilder()
                .AddStep(new CreateUserInfoRequestObject(_userInfoRequestObjectFactory))
                .AddStep(new HttpRequestMessageCreate())
                .Build();
            await startingWorkflow.Execute(new WorkflowContext() { Logger = log });
            // var result = await _userIdServices.GetUserApiResponseAsync();
            // log.LogWarning(await result.Content.ReadAsStringAsync());
        }
    }
}
