using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;

namespace WotPersonalDataCollector.Workflow.Factory
{
    internal sealed class WorkflowStepsFactory: IWorkflowStepsFactory
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;

        public WorkflowStepsFactory(IUserInfoRequestObjectFactory userInfoRequestObjectFactory, IHttpRequestMessageFactory httpRequestMessageFactory)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
            _httpRequestMessageFactory = httpRequestMessageFactory;
        }
        
        public BaseStep CreateUserInfoRequestObject()
        {
            return new CreateUserInfoRequestObjectStep(_userInfoRequestObjectFactory);
        }

        public BaseStep CreateHttpRequestMessage()
        {
            return new HttpRequestMessageCreateStep(_httpRequestMessageFactory);
        }
    }
}
