using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Exceptions;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.Api.User
{
    [TestFixture]
    public class DeserializeHttpResponseTests
    {
        private IUserIdServices _userIdServices;
        private IDeserializeHttpResponse _uut;

        [SetUp]
        public void SetUp()
        {
            _userIdServices = Substitute.For<IUserIdServices>();
            _uut = new DeserializeHttpResponse(_userIdServices);
        }

        [Test]
        public async Task ShouldDeserializeData()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var responseObject = Any.Instance<ResponseObject>();
            var user = Any.Instance<UserData>();
            responseObject.data = new List<UserData>(1) { user };
            responseObject.meta.count = 1;
            responseObject.status = "ok";
            response.Content = new StringContent(JsonConvert.SerializeObject(responseObject), Encoding.UTF8, "application/json");
            _userIdServices.GetUserApiResponseAsync().Returns(response);

            // Act
            var actual = await _uut.Deserialize();

            // Assert
            actual.Should().NotBeNull();
            actual.AccountId.Should().Be(user.AccountId);
            actual.Nickname.Should().Be(user.Nickname);
        }

        [Test]
        public async Task ShouldThrowExceptionWhenResponseNotOk()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            _userIdServices.GetUserApiResponseAsync().Returns(response);

            // Act
            Func<Task> act = async () => await _uut.Deserialize();

            // Assert
            act.Should().ThrowAsync<HttpRequestException>()
                .WithMessage($"Do not received 200 Ok from server instead received {response.StatusCode.ToString()}");
        }

        [Test]
        public async Task ShouldThrowExceptionWhenCantDeserializeData()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var responseObject = Any.Instance<object>();
            response.Content = new StringContent(JsonConvert.SerializeObject(responseObject), Encoding.UTF8, "application/json");
            _userIdServices.GetUserApiResponseAsync().Returns(response);

            // Act
            Func<Task> act = async () => await _uut.Deserialize();

            // Assert
            act.Should().ThrowAsync<DeserializeJsonException>()
                .WithMessage("Can not deserialize data received from Wot API");
        }

        [Test]
        public async Task ShouldThrowExceptionWhenApiReturnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var responseObject = Any.Instance<ResponseObject>();
            responseObject.status = "error";
            response.Content = new StringContent(JsonConvert.SerializeObject(responseObject), Encoding.UTF8, "application/json");
            _userIdServices.GetUserApiResponseAsync().Returns(response);

            // Act
            Func<Task> act = async () => await _uut.Deserialize();

            // Assert
            act.Should().ThrowAsync<WotApiResponseException>()
                .WithMessage("Wot Api returned error message!");
        }

        [Test]
        public async Task ShouldThrowExceptionWhenMoreThanOneUserReceived()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var responseObject = Any.Instance<ResponseObject>();
            var count = Any.Integer();
            responseObject.meta.count = count;
            responseObject.status = "ok";
            response.Content = new StringContent(JsonConvert.SerializeObject(responseObject), Encoding.UTF8, "application/json");
            _userIdServices.GetUserApiResponseAsync().Returns(response);

            // Act
            Func<Task> act = async () => await _uut.Deserialize();

            // Assert
            act.Should().ThrowAsync<WotApiResponseException>()
                .WithMessage($"Received user {count}, provide user id by yourself or check input WotUserName");
        }
    }
}
