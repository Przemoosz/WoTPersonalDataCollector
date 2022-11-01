namespace WotPersonalDataCollectorWebApp.UnitTests.Categories
{
	using NUnit.Framework;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	internal sealed class RuleTestsAttribute: CategoryAttribute
	{
	}
}
