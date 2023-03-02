using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.AzureMicroServicesFactory.Extensions;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Extensions
{
	[TestFixture]
	public class HttpRequestExtensionsTests
	{
		[Test]
		public void ShouldReturnAuthorizationToken()
		{
			// Arrange
			var token = Any.String();
			var items = new Dictionary<object, object>();
			items.Add("Authorization", token);
			HttpRequest httpRequest = new DefaultHttpRequest(new DefaultHttpContext());
			typeof(HttpRequest).GetProperty("")
			
			// Act
			var result = httpRequest.GetAuthorizationToken();

			// Assert
			result.Should().Be(token);
		}
	}
}
