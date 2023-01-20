namespace WotPersonalDataCollectorWebApp.Factories
{
	using Dto;

	/// <inheritdoc/>
	public sealed class PageFactory<T> : IPageFactory<T> where T: class
	{
		private const int PageConstant = 1;

	    public Page<T> CreatePage(IEnumerable<T> dataSource, int pageNumber, int pageSize)
	    {
		    if (pageNumber == 0)
		    {
				return new Page<T>(dataSource.Take(pageSize));
		    }
		    return new Page<T>(dataSource.Skip((pageNumber-PageConstant)*pageSize).Take(pageSize));
	    }

	    public DetailedPage<T> CreateDetailedPage(IEnumerable<T> dataSource, int pageNumber, int pageSize)
	    {
		    throw new NotImplementedException();
	    }
    }
}
