using FluentAssertions;
using NUnit.Framework;
using WotPersonalDataCollector.WebApp.Factories;
using WotPersonalDataCollector.WebApp.Models;
using WotPersonalDataCollector.WebApp.UnitTests.Categories;

namespace WotPersonalDataCollector.WebApp.UnitTests.Factories
{
	[TestFixture, Parallelizable, FactoryTests]
	public class PageFactoryTests
	{
		private PageFactory<VersionValidateResultModel> _uut = null!;

		[SetUp]
		public void SetUp()
		{
			_uut = new PageFactory<VersionValidateResultModel>();
		}

		[TestCase(3)]
		[TestCase(7)]
		public void ShouldCreatePageWithAccurateAmountOfItems(int pageSize)
		{
			// Arrange
			const int pageNumber = 1;
			const int additionalItemsAmount = 3;
			int totalItems = pageSize + additionalItemsAmount;
			List<VersionValidateResultModel> data = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(data,totalItems);

			// Act
			var page = _uut.CreatePage(data, pageNumber, pageSize);

			// Assert
			page.Items.Should().HaveCount(pageSize);
			page.Items.Last().CorrectVersionDtoCount.Should().Be(pageSize);
			page.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(3)]
		[TestCase(5)]
		public void ShouldCreatePageAndSkipCorrectAmountOfElements(int pageNumber)
		{
			// Arrange
			const int pageSize = 3;
			const int additionalItemsAmount = 6;
			int totalItems = pageSize * pageNumber + additionalItemsAmount;
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet,totalItems);

			// Act
			var page = _uut.CreatePage(dataSet, pageNumber, pageSize);

			// Assert
			page.Items.Should().HaveCount(pageSize);
			page.Items.Last().CorrectVersionDtoCount.Should().Be(pageSize * pageNumber);
			page.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(4,7)]
		[TestCase(2,5)]
		public void ShouldCreatePageAndSkipCorrectAmountOfElements(int itemsCount, int pageSize)
		{
			// Arrange
			const int pageNumber = 2;
			int totalItems = pageSize * (pageNumber - 1) + itemsCount;
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);

			// Act
			var page = _uut.CreatePage(dataSet, pageNumber, pageSize);

			// Assert
			page.Items.Should().HaveCount(itemsCount);
			page.Items.Last().CorrectVersionDtoCount.Should().Be(totalItems);
			page.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(3)]
		[TestCase(7)]
		public void ShouldReturnBlankPageIfPageNumberIsBiggerThanMaxPageNumber(int pageNumber)
		{
			// Arrange
			const int pageSize = 2;
			int totalItems = (pageSize - 1) * pageNumber ;
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);
			
			// Act
			var page = _uut.CreatePage(dataSet, pageNumber, pageSize);

			// Assert
			page.Items.Should().HaveCount(0);
			page.PageNumber.Should().Be(pageNumber);
		}

		[Test]
		public void ShouldReturnBlankPageIfDataSetIsEmpty()
		{
			// Arrange
			const int pageNumber = 1;
			const int pageSize = 7;
			List<VersionValidateResultModel> emptyDataSet = new List<VersionValidateResultModel>(0);

			// Act
			var page = _uut.CreatePage(emptyDataSet, pageNumber, pageSize);

			// Assert
			page.Items.Should().HaveCount(0);
			page.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(7,5,5)]
		[TestCase(2, 8, 2)]
		public void ShouldReturnFirstPageIfPageNumberIsZero(int totalItems, int pageSize, int correctAmountOfItems)
		{
			// Arrange
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);

			// Act
			var page = _uut.CreatePage(dataSet, 0, pageSize);

			// Assert
			page.Items.Should().HaveCount(correctAmountOfItems);
			page.Items.Last().CorrectVersionDtoCount.Should().Be(correctAmountOfItems);
		}

		[TestCase(3)]
		[TestCase(7)]
		public void ShouldCreateDetailedPageWithAccurateAmountOfItems(int pageSize)
		{
			// Arrange
			const int pageNumber = 1;
			const int additionalItemsAmount = 3;
			int totalItems = pageSize + additionalItemsAmount;
			List<VersionValidateResultModel> data = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(data, totalItems);

			// Act
			var detailedPage = _uut.CreateDetailedPage(data, pageNumber, pageSize);

			// Assert
			detailedPage.Items.Should().HaveCount(pageSize);
			detailedPage.Items.Last().CorrectVersionDtoCount.Should().Be(pageSize);
			detailedPage.TotalItemsNumber.Should().Be(totalItems);
			detailedPage.ItemsNumber.Should().Be(pageSize);
			detailedPage.PageNumber.Should().Be(pageNumber);
		}


		[TestCase(3)]
		[TestCase(5)]
		public void ShouldCreateDetailedPageAndSkipCorrectAmountOfElements(int pageNumber)
		{
			// Arrange
			const int pageSize = 3;
			const int additionalItemsAmount = 6;
			int totalItems = pageSize * pageNumber + additionalItemsAmount;
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);

			// Act
			var detailedPage = _uut.CreateDetailedPage(dataSet, pageNumber, pageSize);

			// Assert
			detailedPage.Items.Should().HaveCount(pageSize);
			detailedPage.Items.Last().CorrectVersionDtoCount.Should().Be(pageSize * pageNumber);
			detailedPage.TotalItemsNumber.Should().Be(totalItems);
			detailedPage.ItemsNumber.Should().Be(pageSize);
			detailedPage.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(4, 7)]
		[TestCase(2, 5)]
		public void ShouldCreateDetailedPageAndSkipCorrectAmountOfElements(int itemsCount, int pageSize)
		{
			// Arrange
			const int pageNumber = 2;
			int totalItems = pageSize * (pageNumber - 1) + itemsCount;
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);

			// Act
			var detailedPage = _uut.CreateDetailedPage(dataSet, pageNumber, pageSize);

			// Assert
			detailedPage.Items.Should().HaveCount(itemsCount);
			detailedPage.Items.Last().CorrectVersionDtoCount.Should().Be(totalItems);
			detailedPage.TotalItemsNumber.Should().Be(totalItems);
			detailedPage.ItemsNumber.Should().Be(itemsCount);
			detailedPage.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(3)]
		[TestCase(7)]
		public void ShouldReturnBlankDetailedPageIfPageNumberIsBiggerThanMaxPageNumber(int pageNumber)
		{
			// Arrange
			const int pageSize = 2;
			int totalItems = (pageSize - 1) * pageNumber;
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);

			// Act
			var detailedPage = _uut.CreateDetailedPage(dataSet, pageNumber, pageSize);

			// Assert
			detailedPage.Items.Should().HaveCount(0);
			detailedPage.ItemsNumber.Should().Be(0);
			detailedPage.TotalItemsNumber.Should().Be(totalItems);
			detailedPage.PageNumber.Should().Be(pageNumber);
		}

		[Test]
		public void ShouldReturnBlankDetailedPageIfDataSetIsEmpty()
		{
			// Arrange
			const int pageNumber = 1;
			const int pageSize = 7;
			List<VersionValidateResultModel> emptyDataSet = new List<VersionValidateResultModel>(0);

			// Act
			var detailedPage = _uut.CreateDetailedPage(emptyDataSet, pageNumber, pageSize);

			// Assert
			detailedPage.Items.Should().HaveCount(0);
			detailedPage.ItemsNumber.Should().Be(0);
			detailedPage.TotalItemsNumber.Should().Be(0);
			detailedPage.PageNumber.Should().Be(pageNumber);
		}

		[TestCase(7, 5, 5)]
		[TestCase(2, 8, 2)]
		public void ShouldReturnFirstDetailedPageIfPageNumberIsZero(int totalItems, int pageSize, int correctAmountOfItems)
		{
			// Arrange
			List<VersionValidateResultModel> dataSet = new List<VersionValidateResultModel>(totalItems);
			CreateDummyData(dataSet, totalItems);

			// Act
			var detailedPage = _uut.CreateDetailedPage(dataSet, 0, pageSize);

			// Assert
			detailedPage.Items.Should().HaveCount(correctAmountOfItems);
			detailedPage.ItemsNumber.Should().Be(correctAmountOfItems);
			detailedPage.Items.Last().CorrectVersionDtoCount.Should().Be(correctAmountOfItems);
			detailedPage.TotalItemsNumber.Should().Be(totalItems);
		}

		private void CreateDummyData(List<VersionValidateResultModel> dataStorage, int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				dataStorage.Add(new VersionValidateResultModel(){CorrectVersionDtoCount = i + 1});
			}
		}
	}
}
