namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Extensions
{
	using Microsoft.AspNetCore.Http;
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Extensions;
	using Utilities;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
	public class HttpRequestExtensionsTests
	{
		[Test]
		public void ShouldReturnAuthorizationToken()
		{
			// Arrange
			var token = Any.String();
			DefaultHttpRequestOverride httpRequest = new DefaultHttpRequestOverride(new DefaultHttpContext());
			httpRequest.AddAuthorizationHeader(token);

			// Act
			var result = httpRequest.GetAuthorizationToken();

			// Assert
			result.Should().Be(token);
		}
	}
}
