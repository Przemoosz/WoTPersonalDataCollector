namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Utilities
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Http.Internal;

	internal sealed class DefaultHttpRequestOverride: DefaultHttpRequest
	{
		private const string HttpAuthorizationHeader = "Authorization";
		public override IHeaderDictionary Headers { get; }

		public DefaultHttpRequestOverride(HttpContext context) : base(context)
		{ 
			Headers = new HeaderDictionary();
		}

		public void AddAuthorizationHeader(string authorizationToken)
		{
			Headers.Add(HttpAuthorizationHeader, authorizationToken);
		}

	}
}
