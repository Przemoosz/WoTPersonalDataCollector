using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Utilities;


namespace WotPersonalDataCollector
{
    public class WotPersonalDataCrawler
    {
        [FunctionName("WotPersonalDataCrawler")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var configuration = new Configuration();
            var clientWrapperFactory = new HttpClientWrapperFactory();
            var objectMessageFactory = new UserInfoRequestObjectFactory(configuration);
            var apiUrlFactory = new ApiUrlFactory(objectMessageFactory);
            var requestMessageFactory = new HttpRequestMessageFactory(apiUrlFactory);
            var userIdCrawler = new UserIdServices(clientWrapperFactory,requestMessageFactory);
            var result = await userIdCrawler.GetUserApiResponseAsync();
            log.LogWarning(await result.Content.ReadAsStringAsync());
        }
    }
}
