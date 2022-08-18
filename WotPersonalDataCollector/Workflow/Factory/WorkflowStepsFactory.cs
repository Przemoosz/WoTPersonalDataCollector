using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;

namespace WotPersonalDataCollector.Workflow.Factory
{
    internal sealed class WorkflowStepsFactory: IWorkflowStepsFactory
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
        private readonly IWotService _wotService;

        public WorkflowStepsFactory(IUserInfoRequestObjectFactory userInfoRequestObjectFactory, IHttpRequestMessageFactory httpRequestMessageFactory, IWotService wotService)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
            _httpRequestMessageFactory = httpRequestMessageFactory;
            _wotService = wotService;
        }
        
        public BaseStep CreateUserInfoRequestObject()
        {
            return new CreateUserInfoRequestObjectStep(_userInfoRequestObjectFactory);
        }

        public BaseStep CreateHttpRequestMessage()
        {
            return new HttpRequestMessageCreateStep(_httpRequestMessageFactory);
        }
        public BaseStep CreateSendRequestForUserId()
        {
            return new SendRequestForUserIdStep(_wotService);
        }
    }
}
