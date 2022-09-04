using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.Workflow.Steps.Api
{
    [TestFixture]
    public class CreateUserInfoApiUriStepTests
    {
        private IApiUriFactory _apiUriFactory;
        private CreateUserInfoApiUriStep _uut;

        [SetUp]
        public void SetUp()
        {
            _apiUriFactory = Substitute.For<IApiUriFactory>();
            _uut = new CreateUserInfoApiUriStep(_apiUriFactory);
        }

        [Test]
        public async Task ShouldCreateUserInfoApiUri()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserInfoApiUriWithParameters = null;
            var apiUri = Any.String();
            _apiUriFactory.Create(context.UserInfoApiUrl, context.UserInfoRequestObject).Returns(apiUri);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserInfoApiUriWithParameters.Should().NotBeNull();
            context.UserInfoApiUriWithParameters.Should().Be(apiUri);
            _uut.SuccessfulStatus().Should().BeTrue();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserInfoApiUriWithParameters = null;
            _apiUriFactory.Create(context.UserInfoApiUrl, context.UserInfoRequestObject).Throws(Any.Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserInfoApiUriWithParameters.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
