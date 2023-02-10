namespace WotPersonalDataCollectorWebApp.Extensions
{
	using Microsoft.EntityFrameworkCore;

	public static class DbSetExtension
	{
		public static async Task RemoveAllData<T>(this DbSet<T> dbSet) where T : class
		{
			IAsyncEnumerable<T> dbSetAsAsyncEnumerable = dbSet.AsAsyncEnumerable();
			await foreach (var entity in dbSetAsAsyncEnumerable)
			{
				dbSet.Remove(entity);
			}
		}
	}
}
