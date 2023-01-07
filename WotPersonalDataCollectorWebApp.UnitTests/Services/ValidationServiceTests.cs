using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.Models;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;
using WotPersonalDataCollectorWebApp.UnitTests.TestUtilities;

namespace WotPersonalDataCollectorWebApp.UnitTests.Services
{
	[TestFixture, Parallelizable, ServiceTest]
	public sealed class ValidationServiceTests
	{
		private IDtoVersionValidator _dtoVersionValidator = null!;
		private ICosmosContext _cosmosDatabaseContext = null!;
		private IValidationCancellationService _validationCancellationService = null!;
		private ValidationService _uut = null!;

		private const string ValidDataLabel = "ValidData";
		private const string ClassPropertiesWrongDataLabel = "ClassPropertiesWrongData";
		private const string WrongDataLabel = "WrongData";
		private const string WrongVersionDataLabel = "WrongVersion";

		[SetUp]
		public void SetUp()
		{
			_dtoVersionValidator = Substitute.For<IDtoVersionValidator>();
			_cosmosDatabaseContext = Substitute.For<ICosmosContext>();
			_validationCancellationService = Substitute.For<IValidationCancellationService>();
			_uut = new ValidationService(_dtoVersionValidator,_cosmosDatabaseContext,_validationCancellationService);
		}

		[TestCase(7)]
		[TestCase(0)]
		public async Task ShouldCorrectlyCountValidObjects(int itemsCount)
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateValidData(itemsCount));
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			await _cosmosDatabaseContext.VersionValidateResult.Received()
				.AddAsync(Arg.Is<VersionValidateResultModel>(s =>
					s.TotalItemsInCosmosDb == itemsCount && s.WasValidationCanceled == false &&
					s.WrongObjectsCount == 0 && s.WrongVersionDtoCount == 0 && s.CorrectVersionDtoCount == itemsCount));
		}

		[Test]
		public async Task ShouldCorrectlyRecognizeObjectAsWrongForAllConditions()
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateDataWithWrongClassProperties());
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			await _cosmosDatabaseContext.VersionValidateResult.Received()
				.AddAsync(Arg.Is<VersionValidateResultModel>(s =>
					s.TotalItemsInCosmosDb == 3 && s.CorrectVersionDtoCount == 0 &&
					s.WrongObjectsCount == 3 && s.WrongVersionDtoCount == 0 && s.WasValidationCanceled == false));

		}

		[TestCase(7)]
		[TestCase(0)]
		public async Task ShouldCorrectlyCountWrongObjectsWhenDtoVersionComponentsExceptionIsThrown(int wrongObjectsCount)
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateWrongData(wrongObjectsCount));
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			await _cosmosDatabaseContext.VersionValidateResult.Received()
				.AddAsync(Arg.Is<VersionValidateResultModel>(s =>
					s.TotalItemsInCosmosDb == wrongObjectsCount && s.CorrectVersionDtoCount == 0 &&
					s.WrongObjectsCount == wrongObjectsCount && s.WrongVersionDtoCount == 0 && s.WasValidationCanceled == false));
		}

		[TestCase(8)]
		[TestCase(0)]
		public async Task ShouldCorrectlyCountWrongVersionObjectsWhenDtoVersionExceptionIsThrown(int wrongVersionObjectsCount)
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateWrongVersionData(wrongVersionObjectsCount));
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			await _cosmosDatabaseContext.VersionValidateResult.Received()
				.AddAsync(Arg.Is<VersionValidateResultModel>(s =>
					s.TotalItemsInCosmosDb == wrongVersionObjectsCount && s.CorrectVersionDtoCount == 0 &&
					s.WrongObjectsCount == 0 && s.WrongVersionDtoCount == wrongVersionObjectsCount && s.WasValidationCanceled == false));
		}

		[TestCase(3, 7, 9)]
		[TestCase(10, 2, 4)]

		[TestCase(6, 7, 2)]

		public async Task ShouldCorrectlyCountAllData(int validItems, int wrongItems, int versionInvalidItems)
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateWrongData(wrongItems),
				CreateValidData(validItems), CreateWrongVersionData(versionInvalidItems));
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			await _cosmosDatabaseContext.VersionValidateResult.Received()
				.AddAsync(Arg.Is<VersionValidateResultModel>(s =>
					s.TotalItemsInCosmosDb == validItems + wrongItems + versionInvalidItems && s.CorrectVersionDtoCount == validItems &&
					s.WrongObjectsCount == wrongItems && s.WrongVersionDtoCount == versionInvalidItems && s.WasValidationCanceled == false));
		}

		[Test]
		public async Task ShouldStopValidationIfCancellationWasRequested()
		{
			// Arrange
			const int cancellationAfter = 3;
			const int itemsCount = 4;
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateValidData(itemsCount));
			SetUpMocksForRequestValidateProcess(databaseCollection);
			_validationCancellationService.IsCancellationRequested.Returns(s => false, s => false, s => true, s => true);
			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			await _cosmosDatabaseContext.VersionValidateResult.Received()
				.AddAsync(Arg.Is<VersionValidateResultModel>(s =>
					s.TotalItemsInCosmosDb == cancellationAfter && s.WasValidationCanceled == true &&
					s.WrongObjectsCount == 0 && s.WrongVersionDtoCount == 0 && s.CorrectVersionDtoCount == cancellationAfter));
		}

		[Test]
		public async Task IsValidationFinishedShouldBeFalseTrueIfOperationFinished()
		{
			// Arrange
			SetUpMocksForRequestValidateProcess(new AsyncEnumerable<WotDataCosmosDbDto>(0));

			// Act
			await _uut.RunValidationProcessAsync();

			// Assert
			_uut.IsValidationFinished.Should().BeTrue();
		}

		[Test]
		public void IsValidationFinishedShouldBeTrueByDefault()
		{
			// Assert
			_uut.IsValidationFinished.Should().BeTrue();
		}

		private void SetUpMocksForRequestValidateProcess(IAsyncEnumerable<WotDataCosmosDbDto> dataCollection)
		{
			_validationCancellationService.IsCancellationRequested.Returns(false);
			_cosmosDatabaseContext.PersonalData.AsAsyncEnumerable().Returns(dataCollection);
			_dtoVersionValidator.When(v => v.EnsureVersionCorrectness(Arg
				.Is<WotDataCosmosDbDto>(s => s.AccountId.Equals(WrongDataLabel))))
				.Do(t => throw new DtoVersionComponentsException());
			_dtoVersionValidator
				.When(v => v.EnsureVersionCorrectness(Arg.Is<WotDataCosmosDbDto>(s =>
					s.AccountId.Equals(WrongVersionDataLabel)))).Do(t => throw new DtoVersionException());
		}

		private IEnumerable<WotDataCosmosDbDto> CreateValidData(int itemsCount)
		{
			var collection = new List<WotDataCosmosDbDto>(itemsCount);
			for (int i = 0; i < itemsCount; i++)
			{
				collection.Add(new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WotAccount", DtoVersion = "1.0.0" }, AccountId = ValidDataLabel });
			}
			return collection;
		}

		private IEnumerable<WotDataCosmosDbDto> CreateDataWithWrongClassProperties()
		{
			return new List<WotDataCosmosDbDto>(3)
			{
				new WotDataCosmosDbDto() { ClassProperties = null, AccountId = ClassPropertiesWrongDataLabel},
				new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WrongType" }, AccountId = ClassPropertiesWrongDataLabel},
				new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WotAccount", DtoVersion = null }, AccountId = ClassPropertiesWrongDataLabel }
			};
		}

		private IEnumerable<WotDataCosmosDbDto> CreateWrongData(int itemsCount)
		{
			var collection = new List<WotDataCosmosDbDto>(itemsCount);
			for (int i = 0; i < itemsCount; i++)
			{
				collection.Add(new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WotAccount", DtoVersion = "1.0.0" }, AccountId = WrongDataLabel });
			}
			return collection;
		}

		private IEnumerable<WotDataCosmosDbDto> CreateWrongVersionData(int itemsCount)
		{
			var collection = new List<WotDataCosmosDbDto>(itemsCount);
			for (int i = 0; i < itemsCount; i++)
			{
				collection.Add(new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WotAccount", DtoVersion = "1.0.0" }, AccountId = WrongVersionDataLabel });
			}
			return collection;
		}
	}
}
