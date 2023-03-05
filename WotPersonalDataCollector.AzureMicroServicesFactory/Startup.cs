[assembly: Microsoft.Azure.Functions.Extensions.DependencyInjection.FunctionsStartup(typeof(WotPersonalDataCollector.AzureMicroServicesFactory.Startup))]
namespace WotPersonalDataCollector.AzureMicroServicesFactory
{
	using Microsoft.Azure.Functions.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection;
	using Utilities;
	using Authentication.Token;
	using Authorization;
	using Security;
	using Security.Validation;

	internal sealed class Startup: FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			InstallLogging(builder);
			InstallAzureMicroServicesFactoryDependencies(builder);
			InstallLocalData(builder);
			InstallAuthorizationDependencies(builder);
		}

		private void InstallAuthorizationDependencies(IFunctionsHostBuilder builder)
		{
			builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
			builder.Services.AddSingleton<IAuthorizationSecurityService, AuthorizationSecurityService>();
			builder.Services.AddSingleton<IAuthorizationTokenValidationService, AuthorizationTokenValidationService>();
		}

		private void InstallAzureMicroServicesFactoryDependencies(IFunctionsHostBuilder builder)
		{
			builder.Services.AddSingleton<ITokenFactory, TokenFactory>();
		}

		private void InstallLocalData(IFunctionsHostBuilder builder) => builder.Services.AddSingleton<IMicroServicesConfiguration, MicroServicesConfiguration>();
		private void InstallLogging(IFunctionsHostBuilder builder) => builder.Services.AddLogging();
	}
}
