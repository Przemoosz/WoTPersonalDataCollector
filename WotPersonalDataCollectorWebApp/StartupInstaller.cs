namespace WotPersonalDataCollectorWebApp
{
	using CosmosDb.Dto.Version;
	using CosmosDb.Dto.Version.RuleEngine;
	using CosmosDb.Dto.Version.RuleEngine.Factory;
	using Utilities;

	internal sealed  class StartupInstaller: IStartupInstaller
	{
		public void InstallComponents(WebApplicationBuilder builder)
		{
			InstalLocalData(builder);
			InstallCosmosDatabase(builder);
			InstalDependencies(builder);
		}

		private void InstallCosmosDatabase(WebApplicationBuilder builder)
		{

		}

		private void InstalLocalData(WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<IAspConfiguration, AspConfiguration>();
		}

		private void InstalDependencies(WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<IVersionRuleEngine, VersionRuleEngine>();
			builder.Services.AddSingleton<IDtoVersionValidator, DtoVersionValidator>();
			builder.Services.AddSingleton<ISemanticVersionModelFactory, SemanticVersionModelFactory>();
			builder.Services.AddSingleton<IRulesFactory, RulesFactory>();
		}
	}

	public interface IStartupInstaller
	{
		void InstallComponents(WebApplicationBuilder builder);
	}
}
