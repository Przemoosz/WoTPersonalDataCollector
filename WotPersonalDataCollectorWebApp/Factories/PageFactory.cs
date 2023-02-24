using WotPersonalDataCollector.WebApp.Dto;

namespace WotPersonalDataCollector.WebApp.Factories
{
	/// <inheritdoc/>
	public sealed class PageFactory<T> : IPageFactory<T> where T: class
	{
		private const int PageConstant = 1;

	    public Page<T> CreatePage(IEnumerable<T> dataSource, int pageNumber, int pageSize)
	    {
		    if (pageNumber == 0)
		    {
				return new Page<T>(dataSource.Take(pageSize), pageNumber);
		    }
		    return new Page<T>(dataSource.Skip((pageNumber-PageConstant)*pageSize).Take(pageSize), pageNumber);
	    }

	    public DetailedPage<T> CreateDetailedPage(IEnumerable<T> dataSource, int pageNumber, int pageSize)
	    {
			List<T> data = new List<T>();
			int totalItems = 0;
			int itemsOnPage = 0;
		    using (IEnumerator<T> enumerator = dataSource.GetEnumerator())
		    {
			    for (int i = 0; i < (pageNumber - PageConstant) * pageSize; i++)
			    {
				    if (!enumerator.MoveNext())
				    {
						break;
				    }
				    totalItems++;
				}
			    for (int i = 0; i < pageSize; i++)
			    {
				    if (!enumerator.MoveNext())
				    {
					   break;
					}
				    data.Add(enumerator.Current);
				    totalItems++;
				    itemsOnPage++;
			    }
			    while (enumerator.MoveNext())
			    {
					totalItems++;
				}
		    }
		    return new DetailedPage<T>(data, itemsOnPage, totalItems, pageNumber);
	    }
    }
}
