using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api;

namespace WotPersonalDataCollector.Workflow.Steps.Api
{
    internal class CreateUserPersonalDataUriStep: BaseStep
    {
        private readonly IApiUriFactory _apiUriFactory;
        private bool _createdUserPersonalDataApiUriWithParameters = true;

        public CreateUserPersonalDataUriStep(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }
        public override Task ExecuteInner(WorkflowContext context)
        {

            try
            {
                context.UserPersonalDataApiUrlWithParameters =
                    _apiUriFactory.Create(context.UserPersonalDataApiUrl, context.UserPersonalDataRequestObject);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred when creating api uri for personal data collector. Message: {exception.Message}\n At: {exception.StackTrace}");
                _createdUserPersonalDataApiUriWithParameters = false;
                context.UnexpectedException = true;
                return Task.CompletedTask;
            }
        }

        public override bool SuccessfulStatus() => _createdUserPersonalDataApiUriWithParameters;
    }
}
