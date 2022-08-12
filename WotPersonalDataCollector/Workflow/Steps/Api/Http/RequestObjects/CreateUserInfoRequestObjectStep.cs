using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects
{
    internal class CreateUserInfoRequestObjectStep: BaseStep
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;

        public CreateUserInfoRequestObjectStep(IUserInfoRequestObjectFactory userInfoRequestObjectFactory)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserInfoRequestObject = _userInfoRequestObjectFactory.Create();
            }
            catch (Exception exception)
            {
                context.Logger.LogError($"Unexpected error occurred during creating userInfoObject. Message: {exception.Message}\n At: {exception.StackTrace} ")
            }
        }
    }
}
