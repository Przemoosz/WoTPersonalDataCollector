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
        public IRequestObject UserPersonalDataRequestObject { get; set; }
        public string UserInfoApiUriWithParameters { get; set; }
        public string UserPersonalDataApiUrlWithParameters { get; set; }
        public string UserPersonalDataApiUrl { get; set; }
        public HttpRequestMessage UserPersonalDataRequestMessage { get; set; }
        public HttpResponseMessage UserPersonalDataResponseMessage { get; set; }
    }
}
