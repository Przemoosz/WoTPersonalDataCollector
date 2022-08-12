namespace WotPersonalDataCollector.Workflow.Builder
{
    internal class WorkflowBuilderFactory: IWorkflowBuilderFactory
    {
        public IWorkflowBuilder Create()
        {
            return new WorkflowBuilder();
        }
    }
}
