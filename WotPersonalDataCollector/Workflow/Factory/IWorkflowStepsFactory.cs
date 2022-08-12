using WotPersonalDataCollector.Workflow.Steps;

namespace WotPersonalDataCollector.Workflow.Factory;

internal interface IWorkflowStepsFactory
{
    BaseStep CreateUserInfoRequestObject();
    BaseStep CreateHttpRequestMessage();
}