using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;
using WotPersonalDataCollector.Workflow.Steps.Api.User;

namespace WotPersonalDataCollector.Workflow.Factory
{
    internal sealed class WorkflowStepsFactory: IWorkflowStepsFactory
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
        private readonly IWotService _wotService;
        private readonly IDeserializeUserIdHttpResponse _deserializeUserIdHttpResponse;

        public WorkflowStepsFactory(IUserInfoRequestObjectFactory userInfoRequestObjectFactory,
            IHttpRequestMessageFactory httpRequestMessageFactory, IWotService wotService,
            IDeserializeUserIdHttpResponse deserializeUserIdHttpResponse)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
            _httpRequestMessageFactory = httpRequestMessageFactory;
            _wotService = wotService;
            _deserializeUserIdHttpResponse = deserializeUserIdHttpResponse;
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

        public BaseStep CreateDeserializeUserIdResponseMessage()
        {
            return new DeserializeUserIdHttpResponseStep(_deserializeUserIdHttpResponse);
        }
    }
}
