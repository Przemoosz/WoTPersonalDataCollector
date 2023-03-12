namespace WotPersonalDataCollector.TestHelpers.Categories
{
	using NUnit.Framework;

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ConfigurationTestsAttribute: CategoryAttribute
	{
	}
}
