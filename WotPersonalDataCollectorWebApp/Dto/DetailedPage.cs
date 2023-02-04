namespace WotPersonalDataCollectorWebApp.Dto;

/// <summary>
/// Class that represents page, contains only fixed amount of data and additional data like total items
/// and number of items ona page.
/// </summary>
/// <typeparam name="T">Type of data stored in Page object.</typeparam>
public sealed class DetailedPage<T> : Page<T> where T : class
{
	/// <summary>
	/// Gets items number on a page.
	/// </summary>
	/// <value><see cref="int"/> value contains number of items on a page.</value>
	public int ItemsNumber { get; init; }

	/// <summary>
	/// Gets number of total items from data source.
	/// </summary>
	/// <value><see cref="int"/> value contains  number of total items from data source.</value>
	public int TotalItemsNumber { get; init; }

	public DetailedPage(IEnumerable<T> data, int itemsNumber, int totalItemsNumber, int pageNumber) : base(data, pageNumber)
	{
		ItemsNumber = itemsNumber;
		TotalItemsNumber = totalItemsNumber;
	}
}