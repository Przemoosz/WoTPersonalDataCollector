namespace WotPersonalDataCollectorWebApp.UnitTests.Categories
{
	using NUnit.Framework;

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	internal class ControllerTestsAttribute: CategoryAttribute
	{
	}
}
