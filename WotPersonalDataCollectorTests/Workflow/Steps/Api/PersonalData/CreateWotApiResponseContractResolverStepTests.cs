namespace WotPersonalDataCollector.Tests.Workflow.Steps.Api.PersonalData
{
	using System.Threading.Tasks;
	using WotPersonalDataCollector.Workflow;
	using WotPersonalDataCollector.Workflow.Steps.Api.PersonalData;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class CreateWotApiResponseContractResolverStepTests
    {
        private CreateWotApiResponseContractResolverStep _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new CreateWotApiResponseContractResolverStep();
        }

        [Test]
        public async Task ShouldCreateContractResolver()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.ContractResolver = null;

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.ContractResolver.Should().NotBeNull();
        }
    }
}
