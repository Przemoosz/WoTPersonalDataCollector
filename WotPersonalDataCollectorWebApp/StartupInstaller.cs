using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollector.WebApp.CosmosDb.Context;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Factory;
using WotPersonalDataCollector.WebApp.Factories;
using WotPersonalDataCollector.WebApp.Properties;
using WotPersonalDataCollector.WebApp.Services;
using WotPersonalDataCollector.WebApp.Utilities;

namespace WotPersonalDataCollector.WebApp
{
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
