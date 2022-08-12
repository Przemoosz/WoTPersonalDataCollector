using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http
{
    internal class HttpRequestMessageCreateStep: BaseStep
    {
        private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
        private bool _createdRequestMessage = true;

        public HttpRequestMessageCreateStep(IHttpRequestMessageFactory httpRequestMessageFactory)
        {
            _httpRequestMessageFactory = httpRequestMessageFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserInfoRequestMessage = _httpRequestMessageFactory.Create(context.UserInfoApiUrl);
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred when creating http requestMessage. Message: {exception.Message}\n At: {exception.StackTrace}");
                _createdRequestMessage = false;
            }
        }

        public override bool SuccessfulStatus() => _createdRequestMessage;
    }
}
