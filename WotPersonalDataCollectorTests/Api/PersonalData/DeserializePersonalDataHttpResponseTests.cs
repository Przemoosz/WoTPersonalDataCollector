namespace WotPersonalDataCollector.Tests.Api.PersonalData
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using WotPersonalDataCollector.Api.PersonalData;
	using WotPersonalDataCollector.Api.PersonalData.Dto;
	using Exceptions;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class DeserializePersonalDataHttpResponseTests
    {
        private IDeserializePersonalDataHttpResponse _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new DeserializePersonalDataHttpResponse();
        }

        [Test]
        public async Task ShouldDeserializePersonalDataHttpResponse()
        {
            // Arrange
            var accountDto = Any.Instance<WotAccountDto>();
            var responseMessage = Any.Instance<HttpResponseMessage>();
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(accountDto));

            // Act
            var actual = await _uut.Deserialize(responseMessage, null);

            // Assert
            actual.Data.WotUser.CreatedTime.Should().Be(accountDto.Data.WotUser.CreatedTime);
        }

        [Test]
        public async Task ShouldThrowExceptionWhenCantDeserializeData()
        {
            // Arrange
            var accountDto = Any.Instance<object>();
            var responseMessage = Any.Instance<HttpResponseMessage>();
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(accountDto));

            // Act
            Func<Task<WotAccountDto>> act = async () => await _uut.Deserialize(responseMessage, null);

            // Assert
            await act.Should().ThrowAsync<DeserializeJsonException>().WithMessage("Can not deserialize user personal data received from Wot Api!");
        }
    }
}
