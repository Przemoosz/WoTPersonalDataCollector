namespace WotPersonalDataCollector.TestHelpers.Categories
{
	using NUnit.Framework;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class ServiceTestAttribute: CategoryAttribute
	{
	}
}
