using NUnit.Framework;

namespace WotPersonalDataCollector.WebApp.UnitTests.Categories
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	internal sealed class FactoryTestsAttribute: CategoryAttribute
	{
	}
}
