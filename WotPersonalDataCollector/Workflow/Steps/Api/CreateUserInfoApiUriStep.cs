using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api;

namespace WotPersonalDataCollector.Workflow.Steps.Api
{
    internal class CreateUserInfoApiUriStep: BaseStep
    {
        private readonly IApiUriFactory _apiUriFactory;
        private bool _createdUserInfoApiUriWithParameters = true;

        public CreateUserInfoApiUriStep(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }
        public override Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserInfoApiUriWithParameters =
                    _apiUriFactory.Create(context.UserInfoApiUrl, context.UserInfoRequestObject);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred when creating api uri for user id data collector. Message: {exception.Message}\n At: {exception.StackTrace}");
                _createdUserInfoApiUriWithParameters = false;
                context.UnexpectedException = true;
                return Task.CompletedTask;
            }
        }

        public override bool SuccessfulStatus() => _createdUserInfoApiUriWithParameters;
    }
}
