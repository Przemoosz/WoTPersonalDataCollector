namespace WotPersonalDataCollector.WebApp.Dto;

/// <summary>
/// Class that represents page, contains only fixed amount of data.
/// </summary>
/// <typeparam name="T">Type of data stored in Page object.</typeparam>
public class Page<T> where T : class
{
	/// <summary>
	/// Gets current page number.
	/// </summary>
	/// <value><see cref="int"/> number that indicates current page number.</value>
	public int PageNumber { get; init; }

	/// <summary>
	/// Gets items associated with page.
	/// </summary>
	/// <value><see cref="IEnumerable{T}"/> that contains data associated with page.</value>
	public IEnumerable<T> Items { get; init; }

	public Page(IEnumerable<T> data, int pageNumber)
	{
		PageNumber = pageNumber;
		Items = data;
	}
}