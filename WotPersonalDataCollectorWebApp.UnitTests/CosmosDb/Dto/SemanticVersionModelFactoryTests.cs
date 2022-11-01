using FluentAssertions;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.UnitTests.CosmosDb.Dto
{
    [TestFixture, Parallelizable]
    public class SemanticVersionModelFactoryTests
    {
        private ISemanticVersionModelFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new SemanticVersionModelFactory();
        }

        [TestCase("1.0.0", 1, 0, 0)]
        [TestCase("4.9.11", 4, 9, 11)]
        [TestCase("15.124.98", 15, 124, 98)]
        public void ShouldSplitStringProvidedVersionAndCreateSemanticVersionModel(string stringVersion, int major, int minor,
            int patch)
        {
            // Act
            var actual = _uut.Create(stringVersion);

            // Assert
            actual.Major.Should().Be(major);
            actual.Minor.Should().Be(minor);
            actual.Patch.Should().Be(patch);
        }

        [TestCase("1.0.0.0")]
        [TestCase("23.19")]
        public void ShouldThrowDtoVersionComponentsExceptionWhenVersionDoesNotMatchSemanticVersiresharponingPattern(
            string version)
        {
            // Act
            Func<SemanticVersionModel> act = () => _uut.Create(version);

            // Assert
            act.Should().Throw<DtoVersionComponentsException>()
                .WithMessage("Received DTO version from cosmosDb does not match Semantic Versioning format!");
        }

        [Test]
        public void ShouldThrowDtoVersionComponentsExceptionWhenCannotParseMajorComponentToInt()
        {
            // Arrange
            var version = "X.0.0";

            // Act
            Func<SemanticVersionModel> act = () => _uut.Create(version);

            // Arrange 
            act.Should().Throw<DtoVersionComponentsException>()
                .WithMessage("Cannot parse Major version component to Int32!");
        }

        [Test]
        public void ShouldThrowDtoVersionComponentsExceptionWhenCannotParseMinorComponentToInt()
        {
            // Arrange
            var version = "1.X.0";

            // Act
            Func<SemanticVersionModel> act = () => _uut.Create(version);

            // Arrange 
            act.Should().Throw<DtoVersionComponentsException>()
                .WithMessage("Cannot parse Minor version component to Int32!");
        }

        [Test]
        public void ShouldThrowDtoVersionComponentsExceptionWhenCannotParsePatchComponentToInt()
        {
            // Arrange
            var version = "1.0.X";

            // Act
            Func<SemanticVersionModel> act = () => _uut.Create(version);

            // Arrange 
            act.Should().Throw<DtoVersionComponentsException>()
                .WithMessage("Cannot parse Patch version component to Int32!");
        }
    }
}
