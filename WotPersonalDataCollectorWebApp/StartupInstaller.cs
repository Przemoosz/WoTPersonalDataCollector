namespace WotPersonalDataCollectorWebApp
{
	using CosmosDb.Dto.Version;
	using CosmosDb.Dto.Version.RuleEngine;
	using CosmosDb.Dto.Version.RuleEngine.Factory;
	using Utilities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using CosmosDb.Context;
	using Services;
	using Factories;
	using Properties;
	using System.Diagnostics.CodeAnalysis;


	[ExcludeFromCodeCoverage]
	internal sealed class StartupInstaller
	{
		public void InstallComponents(WebApplicationBuilder builder)
		{
			InstallCosmosDatabase(builder);
			InstallLogger(builder);
			InstallOtherModules(builder);
			InstallFactories(builder);
			InstallServices(builder);
		}

		private void InstallCosmosDatabase(WebApplicationBuilder builder)
		{
			IAspConfiguration config = new AspConfiguration();
			builder.Services.AddDbContext<ICosmosDatabaseContext, CosmosDatabaseContext>(b =>
				b.UseCosmos(config.CosmosConnectionString, config.DatabaseName));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<CosmosDatabaseContext>();
			builder.Services.AddSingleton<ICosmosContext, CosmosDatabaseContext>(); // For non controller classes
		}

		private void InstallLogger(WebApplicationBuilder builder)
		{
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();
		}

		private void InstallServices(WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<IValidationCancellationService, ValidationCancellationService>();
			builder.Services.AddSingleton<IValidationService, ValidationService>();
		}

		private void InstallFactories(WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<ISemanticVersionModelFactory, SemanticVersionModelFactory>();
			builder.Services.AddSingleton<IRulesFactory, RulesFactory>();
			builder.Services.AddScoped(typeof(IPageFactory<>), typeof(PageFactory<>));
		}

		private void InstallOtherModules(WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<IAspConfiguration, AspConfiguration>();
			builder.Services.AddSingleton<IVersionRuleEngine, VersionRuleEngine>();
			builder.Services.AddSingleton<IDtoVersionValidator, DtoVersionValidator>();
			builder.Services.AddSingleton<IResourcesWrapper, ResourcesWrapper>();
		}
	}
}
