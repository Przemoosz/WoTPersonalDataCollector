namespace WotPersonalDataCollectorWebApp.Dto;

/// <summary>
/// Class that represents page, contains only fixed amount of data.
/// </summary>
/// <typeparam name="T">Type of data stored in Page object.</typeparam>
public class Page<T> where T : class
{
	/// <summary>
	/// Gets items associated with page.
	/// </summary>
	/// <value><see cref="IEnumerable{T}"/> that contains data associated with page.</value>
	public IEnumerable<T> Items { get; init; }

	public Page(IEnumerable<T> data)
	{
		Items = data;
	}
}