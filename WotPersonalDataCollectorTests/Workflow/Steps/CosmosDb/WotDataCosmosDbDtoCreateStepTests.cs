using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.CosmosDb;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.Tests.Workflow.Steps.CosmosDb
{
    [TestFixture]
    public class WotDataCosmosDbDtoCreateStepTests
    {
        private IWotDataCosmosDbDtoFactory _wotDataCosmosDbDtoFactory;
        private WotDataCosmosDbDtoCreateStep _uut;

        [SetUp]
        public void SetUp()
        {
            _wotDataCosmosDbDtoFactory = Substitute.For<IWotDataCosmosDbDtoFactory>();
            _uut = new WotDataCosmosDbDtoCreateStep(_wotDataCosmosDbDtoFactory);
        }

        [Test]
        public async Task ShouldReturnCosmosDbDto()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.CosmosDbDto = null;
            var cosmosDto = Any.Instance<WotDataCosmosDbDto>();
            _wotDataCosmosDbDtoFactory.Create(cosmosDto.AccountData, context.UserIdData).ReturnsForAnyArgs(cosmosDto);
            
            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.CosmosDbDto.Should().NotBeNull();
            context.CosmosDbDto.Should().Be(cosmosDto);
        }
    }
}
