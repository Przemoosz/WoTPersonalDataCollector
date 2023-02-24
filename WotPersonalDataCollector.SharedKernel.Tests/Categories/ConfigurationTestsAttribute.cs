namespace WotPersonalDataCollector.SharedKernel.Tests.Categories
{
	using System;

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	internal sealed class ConfigurationTestsAttribute: CategoryAttribute
	{
	}
}
