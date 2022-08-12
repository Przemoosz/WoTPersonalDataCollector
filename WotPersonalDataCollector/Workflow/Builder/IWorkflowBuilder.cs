using WotPersonalDataCollector.Workflow.Steps;

namespace WotPersonalDataCollector.Workflow.Builder;

internal interface IWorkflowBuilder
{
    BaseStep Build();
    IWorkflowBuilder AddStep(BaseStep step);
}