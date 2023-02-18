using FluentAssertions;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api.PersonalData;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.Tests.Api.PersonalData
{
    [TestFixture]
    public class WotApiResponseContractResolverTests
    {
        private string _userId;
        private WotApiResponseContractResolver _uut;

        [SetUp]
        public void SetUp()
        {
            _userId = Any.String();
            _uut = new WotApiResponseContractResolver(_userId);
        }

        [Test]
        public void ShouldResolveWotUserPropertyNameAsUserId()
        {
            // Act
            var resolverPropertyName = _uut.GetResolvedPropertyName("WotUser");
            // Assert
            resolverPropertyName.Should().Be(_userId);
        }

        [TestCase("test", "test")]
        [TestCase("Wot", "Wot")]
        public void ShouldOnlyExtendResolvePropertyNameMethod(string givenProperty, string returnProperty)
        {
            // Act
            var resolverPropertyName = _uut.GetResolvedPropertyName(givenProperty);
            // Assert
            resolverPropertyName.Should().Be(returnProperty);
        }
    }
}
