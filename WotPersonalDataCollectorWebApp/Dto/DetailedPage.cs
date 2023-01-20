namespace WotPersonalDataCollectorWebApp.Dto;

public class DetailedPage<T> : Page<T> where T : class
{
	public int ItemsNumber { get; set; }
	public int TotalItemsNumber { get; set; }
}