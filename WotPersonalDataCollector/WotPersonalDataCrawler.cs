using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Services;


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
            var result = await _userIdServices.GetUserApiResponseAsync();
            log.LogWarning(await result.Content.ReadAsStringAsync());
        }
    }
}
