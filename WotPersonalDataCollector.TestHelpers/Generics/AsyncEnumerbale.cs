namespace WotPersonalDataCollector.TestHelpers.Generics
{
	public sealed class AsyncEnumerable<T>: IAsyncEnumerable<T> where T: class
	{
		private readonly List<T> _collection = new List<T>();

		public AsyncEnumerable()
		{
		}

		public AsyncEnumerable(IEnumerable<T> startingCollection)
		{
			_collection = new List<T>(startingCollection);
		}

		public AsyncEnumerable(params IEnumerable<T>[] collections)
		{
			_collection = new List<T>();
			foreach (var collection in collections)
			{
				_collection.AddRange(collection);
			}
		}

		public AsyncEnumerable(int capacity)
		{
			_collection = new List<T>(capacity);
		}
		
		public void Add(T item)
		{
			_collection.Add(item);
		}

		public void AddRange(IEnumerable<T> items)
		{
			_collection.AddRange(items);
		}

		public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
		{
			return new AsyncEnumerator<T>(_collection);
		}
	}
}
