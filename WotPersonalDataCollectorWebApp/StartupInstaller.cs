namespace WotPersonalDataCollectorWebApp
{
	using CosmosDb.Dto.Version;
	using CosmosDb.Dto.Version.RuleEngine;
	using CosmosDb.Dto.Version.RuleEngine.Factory;
	using Utilities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using CosmosDb.Context;

	internal sealed class StartupInstaller: IStartupInstaller
	{
		public void InstallComponents(WebApplicationBuilder builder)
		{
			InstalLocalData(builder);
			InstallCosmosDatabase(builder);
			InstallLogger(builder);
			InstalDependencies(builder);
		}

		private void InstallCosmosDatabase(WebApplicationBuilder builder)
		{
			IAspConfiguration config = new AspConfiguration();
			builder.Services.AddDbContext<ICosmosDatabaseContext, CosmosDatabaseContext>(b =>
				b.UseCosmos(config.CosmosConnectionString, config.DatabaseName));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<CosmosDatabaseContext>();
		}

		private void InstallLogger(WebApplicationBuilder builder)
		{
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();
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
