using WotPersonalDataCollectorWebApp.Utilities;

namespace WotPersonalDataCollectorWebApp
{
	public class StartupInstaller: IStartupInstaller
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

		}
	}

	public interface IStartupInstaller
	{
		void InstallComponents(WebApplicationBuilder builder);
	}
}
