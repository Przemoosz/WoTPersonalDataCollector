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
			InstallLoggin(builder);
			InstallAzureMicroServicesFactoryDependencies(builder);
			InstallLocalData(builder);
			InstallCosmosDbDependencies(builder);
		}

		private void InstallCosmosDbDependencies(IFunctionsHostBuilder builder)
		{
		}

		private void InstallAzureMicroServicesFactoryDependencies(IFunctionsHostBuilder builder)
		{

		}

		private void InstallLocalData(IFunctionsHostBuilder builder) => builder.Services.AddSingleton<IMicroServicesConfiguration, MicroServicesConfiguration>();
		private void InstallLoggin(IFunctionsHostBuilder builder) => builder.Services.AddLogging();
	}
}
