using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Utilities;


namespace WotPersonalDataCollector
{
    internal class WotPersonalDataCrawler
    {
        private readonly IUserIdServices _userIdServices;

        public WotPersonalDataCrawler(IUserIdServices userIdServices)
        {
            _userIdServices = userIdServices;
        }

        [FunctionName("WotPersonalDataCrawler")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            // var configuration = new Configuration();
            // var clientWrapperFactory = new HttpClientWrapperFactory();
            // var objectMessageFactory = new UserInfoRequestObjectFactory(configuration);
            // var apiUrlFactory = new ApiUrlFactory(objectMessageFactory);
            //var requestMessageFactory = new HttpRequestMessageFactory(apiUrlFactory);
            //var userIdCrawler = new UserIdServices(clientWrapperFactory,requestMessageFactory);
            var result = await _userIdServices.GetUserApiResponseAsync();
            log.LogWarning(await result.Content.ReadAsStringAsync());
        }
    }
}
