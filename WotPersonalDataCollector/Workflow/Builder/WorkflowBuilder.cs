using WotPersonalDataCollector.Workflow.Steps;

namespace WotPersonalDataCollector.Workflow.Builder
{
    internal class WorkflowBuilder: IWorkflowBuilder
    {
        private BaseStep _headStep;
        private BaseStep _currentStep;

        public IWorkflowBuilder AddStep(BaseStep step)
        {
            if (_headStep is null)
            {
                _headStep = step;
            }
            else
            {
                _currentStep.SetNext(step);
            }
            _currentStep = step;
            return this;
        }

        public BaseStep Build()
        {
            return _headStep;
        }
    }
}
