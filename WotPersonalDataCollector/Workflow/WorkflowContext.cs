using System.Net.Http;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.User.DTO;

namespace WotPersonalDataCollector.Workflow
{
    internal sealed class WorkflowContext
    {
        public string UserInfoApiUrl { get; init; }
        public ILogger Logger { get; init; }
        public IRequestObject UserInfoRequestObject { get; set; }
        public HttpRequestMessage UserInfoRequestMessage { get; set; }
        public HttpResponseMessage UserIdResponseMessage { get; set; }
        public UserIdData UserIdData { get; set; }
        public bool UnexpectedException { get; set; }
        public IRequestObject PersonalDataRequestObject { get; set; }
        public string UserInfoApiUrlWithParameters { get; set; }
        public string UserPersonalDataApiUrlWithParameters { get; set; }
        public string UserPersonalDataApiUrl { get; set; }
    }
}
