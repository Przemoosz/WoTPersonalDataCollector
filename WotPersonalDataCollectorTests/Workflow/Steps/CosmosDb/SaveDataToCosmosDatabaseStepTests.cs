﻿namespace WotPersonalDataCollector.Tests.Workflow.Steps.CosmosDb
{
	using System.Threading.Tasks;
	using NSubstitute.ExceptionExtensions;
	using TddXt.AnyRoot;
	using WotPersonalDataCollector.CosmosDb.Services;
	using WotPersonalDataCollector.Workflow;
	using WotPersonalDataCollector.Workflow.Steps.CosmosDb;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class SaveDataToCosmosDatabaseStepTests
    {
        private ICosmosDbService _cosmosDbService;
        private SaveDataToCosmosDatabaseStep _uut;

        [SetUp]
        public void SetUp()
        {
            _cosmosDbService = Substitute.For<ICosmosDbService>();
            _uut = new SaveDataToCosmosDatabaseStep(_cosmosDbService);
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            _cosmosDbService.SaveAsync(context.CosmosDbDto).ThrowsAsync(Any.Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
