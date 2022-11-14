﻿using FluentAssertions;
using NUnit.Framework;
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
		public void ShouldReturnCancellationToken()
		{
			// Act
			var actual = _uut.GetValidationCancellationToken();

			// Assert
			actual.Should().BeOfType<CancellationToken>();
			actual.IsCancellationRequested.Should().BeFalse();
			_uut.IsCancellationAvailable.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnCancellationTokenWithExternalOne()
		{
			// Arrange
			CancellationToken externalCancellationToken = new CancellationToken();

			// Act
			var actual = _uut.GetValidationCancellationToken(externalCancellationToken);

			// Assert
			actual.Should().BeOfType<CancellationToken>();
			actual.IsCancellationRequested.Should().BeFalse();
			_uut.IsCancellationAvailable.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnSameCancellationToken()
		{
			// Act
			var actual1 = _uut.GetValidationCancellationToken();
			var actual2 = _uut.GetValidationCancellationToken();
			var actual3 = _uut.GetValidationCancellationToken();

			// Assert
			actual1.Should().Be(actual2);
			actual2.Should().Be(actual3);
			actual3.Should().Be(actual1);
		}

		[Test]
		public void ShouldReturnSameCancellationTokenWhenCreatingWithExternalCt()
		{
			// Arrange
			CancellationToken externalCancellationToken = new CancellationToken();

			// Act
			var actual1 = _uut.GetValidationCancellationToken(externalCancellationToken);
			var actual2 = _uut.GetValidationCancellationToken(externalCancellationToken);
			var actual3 = _uut.GetValidationCancellationToken(externalCancellationToken);

			// Assert
			actual1.Should().Be(actual2);
			actual2.Should().Be(actual3);
			actual3.Should().Be(actual1);
		}

		[Test]
		public void DifferentThreadsShouldGetSameToken()
		{
			// Arrange
			Task<CancellationToken> task1 = new Task<CancellationToken>(() => _uut.GetValidationCancellationToken());
			Task<CancellationToken> task2 = new Task<CancellationToken>(() => _uut.GetValidationCancellationToken());
			
			// Act
			task1.Start();
			task2.Start();
			Task.WaitAll(task1, task2);
			var actual1 = task1.Result;
			var actual2 = task2.Result;

			// Assert
			actual1.Should().Be(actual2);
		}

		[Test]
		public void DifferentThreadsShouldGetSameTokenWithExternalCt()
		{
			// Arrange
			var externalCancellationToken = new CancellationToken();
			Task<CancellationToken> task1 = new Task<CancellationToken>(() => _uut.GetValidationCancellationToken(externalCancellationToken));
			Task<CancellationToken> task2 = new Task<CancellationToken>(() => _uut.GetValidationCancellationToken(externalCancellationToken));

			// Act
			task1.Start();	
			task2.Start();
			Task.WaitAll(task1, task2);
			var actual1 = task1.Result;
			var actual2 = task2.Result;

			// Assert
			actual1.Should().Be(actual2);
		}

		[Test]
		public void ShouldCancelToken()
		{
			// Act
			var actual = _uut.GetValidationCancellationToken();
			_uut.CancelValidation();

			// Assert
			actual.IsCancellationRequested.Should().BeTrue();
			_uut.IsCancellationRequested.Should().BeTrue();
		}

		[Test]
		public void ShouldCancelTokenWithExternalNotCanceledCt()
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
		public void ShouldCancelTokenWithExternalCanceledCt()
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
		public void ShouldDisposeTokenCorrectly()
		{
			// Arrange
			Func<CancellationToken> getCt = () => _uut.GetValidationCancellationToken();

			// Act
			_uut.GetValidationCancellationToken();
			_uut.Dispose();

			// Assert
			getCt.Should().Throw<ObjectDisposedException>();
		}

		[Test]
		public void ShouldDisposeTokenCorrectlyWithExternalCt()
		{
			// Arrange
			CancellationToken externalCancellationToken = new CancellationToken(false);
			Func<CancellationToken> getCt = () => _uut.GetValidationCancellationToken(externalCancellationToken);

			// Act
			_uut.GetValidationCancellationToken(externalCancellationToken);
			_uut.Dispose();

			// Assert
			getCt.Should().Throw<ObjectDisposedException>();
		}

		[Test]
		public void ShouldNotThrowDisposeExceptionWhenTokenWasNotInitialized()
		{
			// Arrange
			Func<CancellationToken> getCt = () => _uut.GetValidationCancellationToken();

			// Act
			_uut.Dispose();

			// Assert
			getCt.Should().NotThrow<ObjectDisposedException>();
		}

		[Test]
		public void IsCancellationRequestedShouldBeFalseWhenNoGetCtWasCalled()
		{
			// Assert
			_uut.IsCancellationRequested.Should().BeFalse();
		}

		[Test]
		public void IsCancellationRequestedShouldBeFalseWhenNoCancelWasCalled()
		{
			// Act
			_uut.GetValidationCancellationToken();

			// Assert
			_uut.IsCancellationRequested.Should().BeFalse();
		}

		[Test]
		public void IsCancellationRequestedShouldBeFalseWhenNoCancelWasCalledWithExtrenalCt()
		{
			// Arrange
			CancellationToken externalCancellationToken = new CancellationToken(false);

			// Act
			_uut.GetValidationCancellationToken(externalCancellationToken);

			// Assert
			_uut.IsCancellationRequested.Should().BeFalse();
		}

		[Test]
		public void IsCancellationAvailableShouldBeFalseWhenNoGetCtWasCalled()
		{
			// Assert
			_uut.IsCancellationAvailable.Should().BeFalse();
		}

		[Test]
		public void ShouldSetIsCancellationAvailableToFalseAfterDispose()
		{
			// Act
			_uut.GetValidationCancellationToken();
			_uut.Dispose();

			// Assert
			_uut.IsCancellationAvailable.Should().BeFalse();
		}

		[Test]
		public void ShouldAvoidRunConditionWhenGettingIsCancellationAvailableWithGettingFirst()
		{
			// Arrange
			Task<bool> gettingTask = new Task<bool>(() => _uut.IsCancellationAvailable);
			Task<CancellationToken> settingTask = new Task<CancellationToken>(() => _uut.GetValidationCancellationToken());

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
		public void ShouldAvoidRunConditionWhenGettingIsCancellationAvailableWithSettingFirst()
		{
			// Arrange
			Task<CancellationToken> settingTask = new Task<CancellationToken>(() => _uut.GetValidationCancellationToken());
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
	}
}
