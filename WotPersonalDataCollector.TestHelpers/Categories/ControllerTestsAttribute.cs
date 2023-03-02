namespace WotPersonalDataCollector.TestHelpers.Categories
{
	using NUnit.Framework;

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class ControllerTestsAttribute: CategoryAttribute
	{
	}
}
