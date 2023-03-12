namespace WotPersonalDataCollector.AzureMicroServicesFactory.Extensions
{
	using Microsoft.AspNetCore.Http;

	internal static class HttpRequestExtensions
	{
		public static string GetAuthorizationToken(this HttpRequest request) => request.Headers["Authorization"];
	}
}
