using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http
{
    internal class HttpRequestMessageCreate: BaseStep
    {
        public override async Task ExecuteInner(WorkflowContext context)
        {
            context.Logger.LogError("wrrr");
        }
    }
}
