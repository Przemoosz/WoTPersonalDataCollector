using System.Net.Http;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Workflow
{
    internal sealed class WorkflowContext
    {
        public string UserInfoApiUrl { get; init; }
        public ILogger Logger { get; init; }
        public IRequestObject UserInfoRequestObject { get; set; }
        public HttpRequestMessage UserInfoRequestMessage { get; set; }
        public HttpResponseMessage UserIdResponseMessage { get; set; }
    }
}
