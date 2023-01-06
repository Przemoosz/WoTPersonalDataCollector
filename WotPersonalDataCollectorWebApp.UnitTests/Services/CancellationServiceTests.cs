using FluentAssertions;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;

namespace WotPersonalDataCollectorWebApp.UnitTests.Services
{
	[TestFixture, ServiceTest, Parallelizable]
	public class CancellationServiceTests
	{
		private IValidationCancellationService _uut = null!;

		[SetUp]
		public void SetUp()
		{
			_uut = new ValidationCancellationService();
		}

		[TearDown]
		public void TearDown()
		{
			_uut.Dispose();
		}

		[Test]
		public void IsCancellationAvailableShouldBeFalseWhenNoGetCtWasCalled()
		{
			// Assert
			_uut.IsCancellationAvailable.Should().BeFalse();
		}

		[Test]
		public void IsCancellationRequestedShouldBeFalseWhenNoGetCtWasCalled()
		{
			// Assert
			_uut.IsCancellationRequested.Should().BeFalse();
		}

		[Test]
		public void TokenShouldNotBeCanceledWhenExternalTokenIsNotCanceled()
		{
			// Arrange
			CancellationToken externalCancellationToken = new CancellationToken(false);

			// Act
			var actual = _uut.GetValidationCancellationToken(externalCancellationToken);
			_uut.CancelValidation();

			// Assert
			actual.IsCancellationRequested.Should().BeTrue();
			_uut.IsCancellationRequested.Should().BeTrue();
		}

		[Test]
		public void TokenShouldBeCanceledWhenExternalTokenIsCanceled()
		{
			// Arrange
			CancellationToken externalCancellationToken = new CancellationToken(true);

			// Act
			var actual = _uut.GetValidationCancellationToken(externalCancellationToken);

			// Assert
			actual.IsCancellationRequested.Should().BeTrue();
			_uut.IsCancellationRequested.Should().BeTrue();
		}

		[Test]
		public void ShouldThrowValidationCancellationExceptionWhenTokenWasNotInitialized()
		{
			// Arrange
			Action action = () => _uut.CancelValidation();

			// Assert
			action.Should().Throw<ValidationCancellationException>().WithMessage("Can not cancel operation that was not initialized - CancellationTokenService was not called");

		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldReturnCancellationToken(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Act
			var actual = getValidationTokenFunction(_uut, false);

			// Assert
			actual.Should().BeOfType<CancellationToken>();
			actual.IsCancellationRequested.Should().BeFalse();
			_uut.IsCancellationAvailable.Should().BeTrue();
		}
		
		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldReturnSameCancellationToken(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Act
			var actual1 = getValidationTokenFunction(_uut, false);
			var actual2 = getValidationTokenFunction(_uut, false);
			var actual3 = getValidationTokenFunction(_uut, false);

			// Assert
			actual1.Should().Be(actual2);
			actual2.Should().Be(actual3);
			actual3.Should().Be(actual1);
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void DifferentThreadsShouldGetSameToken(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Arrange
			Task<CancellationToken> task1 = new Task<CancellationToken>(() => getValidationTokenFunction(_uut, false));
			Task<CancellationToken> task2 = new Task<CancellationToken>(() => getValidationTokenFunction(_uut, false));
			
			// Act
			task1.Start();
			task2.Start();
			Task.WaitAll(task1, task2);
			var actual1 = task1.Result;
			var actual2 = task2.Result;

			// Assert
			actual1.Should().Be(actual2);
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldCancelToken(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Act
			var actual = getValidationTokenFunction(_uut,false);
			_uut.CancelValidation();

			// Assert
			actual.IsCancellationRequested.Should().BeTrue();
			_uut.IsCancellationRequested.Should().BeTrue();
		}
		
		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldDisposeTokenCorrectly(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Arrange
			Func<CancellationToken> getCt = () => getValidationTokenFunction(_uut, false);

			// Act
			getValidationTokenFunction(_uut, false);
			_uut.Dispose();

			// Assert
			getCt.Should().Throw<ObjectDisposedException>();
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldNotThrowDisposeExceptionWhenTokenWasNotInitialized(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Arrange
			Func<CancellationToken> getCt = () => getValidationTokenFunction(_uut, false);

			// Act
			_uut.Dispose();

			// Assert
			getCt.Should().NotThrow<ObjectDisposedException>();
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void IsCancellationRequestedShouldBeFalseWhenNoCancelWasCalled(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Act
			getValidationTokenFunction(_uut, false);

			// Assert
			_uut.IsCancellationRequested.Should().BeFalse();
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldSetIsCancellationAvailableToFalseAfterDispose(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Act
			getValidationTokenFunction(_uut, false);
			_uut.Dispose();

			// Assert
			_uut.IsCancellationAvailable.Should().BeFalse();
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldAvoidRunConditionWhenGettingIsCancellationAvailableWithGettingFirst(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Arrange
			Task<bool> gettingTask = new Task<bool>(() => _uut.IsCancellationAvailable);
			Task<CancellationToken> settingTask = new Task<CancellationToken>(() => getValidationTokenFunction(_uut, false));

			// Act
			gettingTask.Start();
			Thread.Sleep(2); // Slowing down to ensure that getting thread will run first 
			settingTask.Start();
			Task.WaitAll(settingTask, gettingTask);
			bool result = gettingTask.Result;

			// Assert
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldAvoidRunConditionWhenGettingIsCancellationRequestedWithGettingFirst()
		{
			// Arrange
			_uut.GetValidationCancellationToken();
			Task<bool> gettingTask = new Task<bool>(() => _uut.IsCancellationRequested);
			Task cancellingTask = new Task(() => _uut.CancelValidation());

			// Act
			gettingTask.Start();
			Thread.Sleep(2); // Slowing down to ensure that getting thread will run first 
			cancellingTask.Start();
			Task.WaitAll(cancellingTask, gettingTask);
			bool result = gettingTask.Result;

			// Assert
			result.Should().BeFalse();
		}

		[TestCaseSource(nameof(MethodsOverloadsTestSource))]
		public void ShouldAvoidRunConditionWhenGettingIsCancellationAvailableWithSettingFirst(Func<IValidationCancellationService, bool, CancellationToken> getValidationTokenFunction)
		{
			// Arrange
			Task<CancellationToken> settingTask = new Task<CancellationToken>(() => getValidationTokenFunction(_uut, false));
			Task<bool> gettingTask = new Task<bool>(() => _uut.IsCancellationAvailable);

			// Act
			settingTask.Start();
			Thread.Sleep(2); // Slowing down to ensure that setting thread will run first 
			gettingTask.Start();
			Task.WaitAll(settingTask, gettingTask);
			bool result = gettingTask.Result;

			// Assert
			result.Should().BeTrue();
		}
		
		private static IEnumerable<TestCaseData> MethodsOverloadsTestSource()
		{
			Func<IValidationCancellationService, bool, CancellationToken> singleCancellationToken = (v, isCancelled) => v.GetValidationCancellationToken();
			Func<IValidationCancellationService, bool, CancellationToken> cancellationTokenWithExternalOne = (v, isCancelled) => v.GetValidationCancellationToken(new CancellationToken(isCancelled));
			yield return new TestCaseData(singleCancellationToken);
			yield return new TestCaseData(cancellationTokenWithExternalOne);
		}
	}
}
