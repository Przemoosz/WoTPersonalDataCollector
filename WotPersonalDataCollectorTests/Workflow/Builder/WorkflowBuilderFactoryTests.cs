using FluentAssertions;
using NUnit.Framework;
using WotPersonalDataCollector.Workflow.Builder;

namespace WotPersonalDataCollectorTests.Workflow.Builder
{
    [TestFixture]
    public class WorkflowBuilderFactoryTests
    {
        private IWorkflowBuilderFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new WorkflowBuilderFactory();
        }

        [Test]
        public void ShouldCreateWorkflowBuilder()
        {
            // Act
            var actual = _uut.Create();

            // Assert
            actual.Should().BeAssignableTo<IWorkflowBuilder>();
            actual.Should().BeOfType<WorkflowBuilder>();
        }
    }
}
