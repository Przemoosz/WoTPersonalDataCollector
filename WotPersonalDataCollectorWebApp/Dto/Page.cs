namespace WotPersonalDataCollectorWebApp.Dto;

public class Page<T> where T : class
{
	public IEnumerable<T> Items { get; set; }

	public Page(IEnumerable<T> data)
	{
		Items = data;
	}
}