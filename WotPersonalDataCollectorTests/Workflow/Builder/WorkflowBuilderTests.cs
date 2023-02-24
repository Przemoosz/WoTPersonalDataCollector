namespace WotPersonalDataCollector.Tests.Workflow.Builder
{
	using WotPersonalDataCollector.Workflow.Builder;
	using WotPersonalDataCollector.Workflow.Steps;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class WorkflowBuilderTests
    {
        private IWorkflowBuilder _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new WorkflowBuilder();
        }

        [Test]
        public void ShouldCreateWorkflow()
        {
            // Arrange
            var step = Any.Instance<BaseStep>();

            // Act
            var actual = _uut.AddStep(step).Build();

            // Assert
            Assert.That(actual, Is.Not.Null);
        }
    }
}
