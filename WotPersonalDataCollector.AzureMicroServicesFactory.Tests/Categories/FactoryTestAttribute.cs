namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Categories
{
	using System;

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class FactoryTestAttribute: CategoryAttribute
	{
	}
}
