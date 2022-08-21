using System.Threading.Tasks;

namespace WotPersonalDataCollector.Workflow.Steps
{
    internal abstract class BaseStep
    {
        private BaseStep _next;

        public async Task Execute(WorkflowContext context)
        {
            await ExecuteInner(context);
            if (_next is not null && SuccessfulStatus())
            {
                await _next.Execute(context);
            }
        }

        public abstract Task ExecuteInner(WorkflowContext context);

        public void SetNext(BaseStep nextStep)
        {
            _next = nextStep;
        }

        public virtual bool SuccessfulStatus()
        {
            return true;
        }
    }
}
