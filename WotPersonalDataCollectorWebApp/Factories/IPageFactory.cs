namespace WotPersonalDataCollectorWebApp.Factories
{
    using Dto;
    /// <summary>
    /// Factory responsible for creating <see cref="Page{T}"/> or <see cref="DetailedPage{T}"/> that pages input data
    /// </summary>
    /// <typeparam name="T">Type of dto</typeparam>
    public interface IPageFactory<T> where T : class
    {
        /// <summary>
        /// Converts <paramref name="dataSource"/> <param name="dataSource"/> into <see cref="Page{T}"/> object, that contains only part of total items 
        /// </summary>
        /// <param name="dataSource">Input data source</param>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Amount of items displayed on page</param>
        /// <returns><see cref="Page{T}"/> object, that contains only part of total items</returns>
        Page<T> CreatePage(IEnumerable<T> dataSource, int pageNumber, int pageSize);

		/// <summary>
		/// Converts <paramref name="dataSource"/> <param name="dataSource"/> into <see cref="DetailedPage{T}"/> object, that contains only part of total items and information about data source
		/// </summary>
		/// <param name="dataSource">Input data source</param>
		/// <param name="pageNumber">Number of page</param>
		/// <param name="pageSize">Amount of items displayed on page</param>
		/// <returns><see cref="DetailedPage{T}"/> object, that contains only part of total items and information about data source</returns>
		DetailedPage<T> CreateDetailedPage(IEnumerable<T> dataSource, int pageNumber, int pageSize);
    }
}

