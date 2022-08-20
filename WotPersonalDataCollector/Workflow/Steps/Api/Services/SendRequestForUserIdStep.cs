using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Services;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Services
{
    internal class SendRequestForUserIdStep: BaseStep
    {
        private readonly IWotService _wotService;
        private bool _getCurrentResponse = true;

        public SendRequestForUserIdStep(IWotService wotService)
        {
            _wotService = wotService;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserIdResponseMessage =
                    await _wotService.GetUserIdApiResponseAsync(context.UserInfoRequestMessage);
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
