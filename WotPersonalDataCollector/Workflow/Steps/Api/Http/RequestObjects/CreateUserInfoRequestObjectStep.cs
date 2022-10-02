using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects
{
    internal class CreateUserInfoRequestObjectStep : BaseStep
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private bool _createdUserInfoRequestObject = true;

        public CreateUserInfoRequestObjectStep(IUserInfoRequestObjectFactory userInfoRequestObjectFactory)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
        }
        public override Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserInfoRequestObject = _userInfoRequestObjectFactory.Create();
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred during creating userInfoObject. Message: {exception.Message}\n At: {exception.StackTrace} ");
                _createdUserInfoRequestObject = false;
                context.UnexpectedException = true;
                return Task.CompletedTask;
            }
        }
        public override bool SuccessfulStatus() => _createdUserInfoRequestObject;
    }
}
