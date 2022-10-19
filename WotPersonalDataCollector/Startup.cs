using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.PersonalData;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.CosmosDb;
using WotPersonalDataCollector.CosmosDb.DatabaseContext;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.CosmosDb.Services;
using WotPersonalDataCollector.Utilities;
using WotPersonalDataCollector.Workflow.Factory;

[assembly: FunctionsStartup(typeof(WotPersonalDataCollector.Startup))]
namespace WotPersonalDataCollector
{
    internal class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            InstallLocalData(builder);
            InstallCosmosDbDependencies(builder);
            InstallDataCrawlerDependencies(builder);
        }

        private void InstallCosmosDbDependencies(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IWpdCosmosClientWrapper, WpdCosmosClientWrapper>();
            builder.Services.AddSingleton<IWpdCosmosClientWrapperFactory, WpdCosmosClientWrapperFactory>();
            builder.Services.AddSingleton<ICosmosContainerService, CosmosContainerService>();
            builder.Services.AddSingleton<IWotContextWrapper, WotContextWrapper>();
            builder.Services.AddSingleton<IWotContextWrapperFactory, WotContextWrapperFactory>();
            builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
        }

        private void InstallLocalData(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IConfiguration, Configuration>();
        }

        private void InstallDataCrawlerDependencies(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IHttpClientWrapperFactory, HttpClientWrapperFactory>();
            builder.Services.AddSingleton<IUserInfoRequestObjectFactory, UserInfoRequestObjectFactory>();
            builder.Services.AddSingleton<IApiUriFactory, ApiUriFactory>();
            builder.Services.AddSingleton<IUserRequestMessageFactory, UserRequestMessageFactory>();
            builder.Services.AddSingleton<IWotService, WotService>();
            builder.Services.AddSingleton<IWorkflowStepsFactory, WorkflowStepsFactory>();
            builder.Services.AddSingleton<IDeserializeUserIdHttpResponse, DeserializeUserIdHttpResponse>();
            builder.Services.AddSingleton<IUserPersonalDataRequestObjectFactory, UserPersonalDataRequestObjectFactory>();
            builder.Services.AddSingleton<IDeserializePersonalDataHttpResponse, DeserializePersonalDataHttpResponse>();
            builder.Services.AddSingleton<IWotDataCosmosDbDtoFactory, WotDataCosmosDbDtoFactory>();
        }
    }
}
