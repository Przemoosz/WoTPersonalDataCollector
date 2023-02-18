using System;
using FluentAssertions;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollectorTests.Api.Http
{
    [TestFixture]
   public class HttpClientWrapperFactoryTests
    {
        private IHttpClientWrapperFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new HttpClientWrapperFactory();
        }

        [Test]
        public void ShouldCreateHttpWrapper()
        {
            // Act
            using var actual = _uut.Create();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<IHttpClientWrapper>();
            actual.Should().BeAssignableTo<IDisposable>();
        }

        [Test]
        public void ShouldReturnSameInstanceOfHttpClient()
        {
            // Act
            using var actual = _uut.Create();
            using var actual2 = _uut.Create();
            using var actual3 = _uut.Create();

            // Assert
            actual.Should().BeSameAs(actual2);
            actual.Should().BeSameAs(actual3);
            actual2.Should().BeSameAs(actual3);
        }
    }
}
