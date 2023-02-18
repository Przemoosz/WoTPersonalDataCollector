using NUnit.Framework;

namespace WotPersonalDataCollector.WebApp.UnitTests.Categories
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	internal sealed class RuleTestsAttribute: CategoryAttribute
	{
	}
}
