using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Workflow
{
    internal sealed class WorkflowContext
    {
        public IRequestObject UserInfoRequestObject { get; set; }
        public ILogger Logger { get; init; }
    }
}
