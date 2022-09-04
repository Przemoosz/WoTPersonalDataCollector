﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Utilities;
using WotPersonalDataCollector.Workflow.Factory;

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
            builder.Services.AddSingleton<IApiUriFactory, ApiUriFactory>();
            builder.Services.AddSingleton<IUserRequestMessageFactory, UserRequestMessageFactory>();
            builder.Services.AddSingleton<IWotService, WotService>();
            builder.Services.AddSingleton<IWorkflowStepsFactory, WorkflowStepsFactory>();
            builder.Services.AddSingleton<IDeserializeUserIdHttpResponse, DeserializeUserIdHttpResponse>();
            builder.Services
                .AddSingleton<IUserPersonalDataRequestObjectFactory, UserPersonalDataRequestObjectFactory>();
        }
    }
}
