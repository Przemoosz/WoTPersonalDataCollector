using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http
{
    internal class UserInfoRequestMessageCreateStep: BaseStep
    {
        private readonly IUserInfoRequestMessageFactory _userInfoRequestMessageFactory;
        private bool _createdRequestMessage = true;

        public UserInfoRequestMessageCreateStep(IUserInfoRequestMessageFactory userInfoRequestMessageFactory)
        {
            _userInfoRequestMessageFactory = userInfoRequestMessageFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserInfoRequestMessage = _userInfoRequestMessageFactory.Create(context.UserInfoApiUriWithParameters);
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred when creating http requestMessage. Message: {exception.Message}\n At: {exception.StackTrace}");
                _createdRequestMessage = false;
                context.UnexpectedException = true;
            }
        }

        public override bool SuccessfulStatus() => _createdRequestMessage;
    }
}
