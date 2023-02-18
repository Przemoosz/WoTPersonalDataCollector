using Microsoft.EntityFrameworkCore;

namespace WotPersonalDataCollector.WebApp.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="DbSet{TEntity}"/>.
	/// </summary>
	public static class DbSetExtension
	{
		/// <summary>
		/// Removes all data from provided <see cref="DbSet{TEntity}"/>.
		/// </summary>
		/// <typeparam name="T">Entity type in data base set.</typeparam>
		/// <param name="dbSet">Database set.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
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
