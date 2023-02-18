using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Exceptions;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.Tests.Api.User
{
    [TestFixture]
    public class DeserializeHttpResponseTests
    {
        private IDeserializeUserIdHttpResponse _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new DeserializeUserIdHttpResponse();
        }

        [Test]
        public async Task ShouldDeserializeData()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var responseObject = Any.Instance<ResponseObject>();
            var user = Any.Instance<UserIdData>();
            responseObject.data = new List<UserIdData>(1) { user };
            responseObject.meta.count = 1;
            responseObject.status = "ok";
            response.Content = new StringContent(JsonConvert.SerializeObject(responseObject), Encoding.UTF8, "application/json");

            // Act
            var actual = await _uut.Deserialize(response);

            // Assert
            actual.Should().NotBeNull();
            actual.AccountId.Should().Be(user.AccountId);
            actual.Nickname.Should().Be(user.Nickname);
        }

        [Test]
        public async Task ShouldThrowExceptionWhenCantDeserializeData()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var responseObject = Any.Instance<object>();
            response.Content = new StringContent(JsonConvert.SerializeObject(responseObject), Encoding.UTF8, "application/json");

            // Act
            Func<Task> act = async () => await _uut.Deserialize(response);

            // Assert
            await act.Should().ThrowAsync<DeserializeJsonException>()
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

            // Act
            Func<Task> act = async () => await _uut.Deserialize(response);

            // Assert
            await act.Should().ThrowAsync<WotApiResponseException>()
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

            // Act
            Func<Task> act = async () => await _uut.Deserialize(response);

            // Assert
            await act.Should().ThrowAsync<MoreThanOneUserException>()
                .WithMessage($"Received {count} user, provide user id by yourself or check input WotUserName");
        }
    }
}
