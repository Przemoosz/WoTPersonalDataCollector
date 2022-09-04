using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Services;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Services
{
    internal class SendRequestForUserPersonalDataStep: BaseStep
    {
        private readonly IWotService _wotService;
        private bool _getCurrentResponse = true;

        public SendRequestForUserPersonalDataStep(IWotService wotService)
        {
            _wotService = wotService;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserPersonalDataResponseMessage =
                    await _wotService.GetUserApiResponseAsync(context.UserPersonalDataRequestMessage);
                Console.WriteLine(await context.UserPersonalDataResponseMessage.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException exception)
            {
                context.Logger.LogError("Error occurred during WOT API connection, do not received 200 OK from API, aborting further processing");
                _getCurrentResponse = false;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred during connecting with WOT API. Message: {exception.Message}\n At: {exception.StackTrace} ");
                _getCurrentResponse = false;
                context.UnexpectedException = true;
            }
        }

        public override bool SuccessfulStatus() => _getCurrentResponse;
    }
}
