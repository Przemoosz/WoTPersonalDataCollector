using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Utilities;

[assembly: FunctionsStartup(typeof(WotPersonalDataCollector.Startup))]
namespace WotPersonalDataCollector
{
    internal class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IConfiguration, Configuration>();
            builder.Services.AddSingleton<IHttpClientWrapperFactory, HttpClientWrapperFactory>();
            builder.Services.AddSingleton<IUserInfoRequestObjectFactory, UserInfoRequestObjectFactory>();
            builder.Services.AddSingleton<IApiUrlFactory, ApiUrlFactory>();
            builder.Services.AddSingleton<IHttpRequestMessageFactory, HttpRequestMessageFactory>();
            builder.Services.AddSingleton<IUserIdServices, UserIdServices>();
        }
    }
}
