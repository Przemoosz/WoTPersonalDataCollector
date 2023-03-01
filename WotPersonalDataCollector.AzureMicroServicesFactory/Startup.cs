using WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token;
using WotPersonalDataCollector.AzureMicroServicesFactory.Authorization;
using WotPersonalDataCollector.AzureMicroServicesFactory.Security;

[assembly: Microsoft.Azure.Functions.Extensions.DependencyInjection.FunctionsStartup(typeof(WotPersonalDataCollector.AzureMicroServicesFactory.Startup))]
namespace WotPersonalDataCollector.AzureMicroServicesFactory
{
	using Microsoft.Azure.Functions.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection;
	using Utilities;

	internal sealed class Startup: FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			InstallLogging(builder);
			InstallAzureMicroServicesFactoryDependencies(builder);
			InstallLocalData(builder);
			InstallCosmosDbDependencies(builder);
		}

		private void InstallCosmosDbDependencies(IFunctionsHostBuilder builder)
		{
		}

		private void InstallAzureMicroServicesFactoryDependencies(IFunctionsHostBuilder builder)
		{
			builder.Services.AddSingleton<ITokenFactory, TokenFactory>();
			builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
			builder.Services.AddSingleton<IAuthorizationSecurityService, AuthorizationSecurityService>();
		}

		private void InstallLocalData(IFunctionsHostBuilder builder) => builder.Services.AddSingleton<IMicroServicesConfiguration, MicroServicesConfiguration>();
		private void InstallLogging(IFunctionsHostBuilder builder) => builder.Services.AddLogging();
	}
}
