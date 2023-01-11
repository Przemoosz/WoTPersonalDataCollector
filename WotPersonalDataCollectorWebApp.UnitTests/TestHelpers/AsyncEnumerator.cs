namespace WotPersonalDataCollectorWebApp.UnitTests.TestUtilities
{
	internal sealed class AsyncEnumerator<T>: IAsyncEnumerator<T> where T: class
	{
		private readonly List<T> _source;
		private int _index;
		public AsyncEnumerator(List<T> source)
		{
			_source = source;
			_index = -1;
		}

		public ValueTask DisposeAsync()
		{
			return ValueTask.CompletedTask;
		}

		public ValueTask<bool> MoveNextAsync()
		{
			_index++;
			return new ValueTask<bool>(_index<_source.Count);
		}

		public T Current => _source[_index];
	}
}
