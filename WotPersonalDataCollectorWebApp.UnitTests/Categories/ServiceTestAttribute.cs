using NUnit.Framework;

namespace WotPersonalDataCollectorWebApp.UnitTests.Categories
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	internal sealed class ServiceTestAttribute: CategoryAttribute
	{
	}
}
