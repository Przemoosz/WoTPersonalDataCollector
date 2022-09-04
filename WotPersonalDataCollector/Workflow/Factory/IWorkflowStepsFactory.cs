using WotPersonalDataCollector.Workflow.Steps;

namespace WotPersonalDataCollector.Workflow.Factory;

internal interface IWorkflowStepsFactory
{
    BaseStep CreateUserInfoRequestObject();
    BaseStep CreateUserInfoHttpRequestMessage();
    BaseStep CreateSendRequestForUserId();
    BaseStep CreateDeserializeUserIdResponseMessage();
    BaseStep CreateUserPersonalDataRequestObject();
    BaseStep CreateUserPersonalDataHttpRequestMessage();
    BaseStep CreateUserInfoApiUri();
    BaseStep CreateUserPersonalDataApiUri();
}