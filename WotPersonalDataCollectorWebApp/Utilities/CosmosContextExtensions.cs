using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;

namespace WotPersonalDataCollectorWebApp.Utilities
{
	public static class CosmosContextExtensions
	{
		public static void RemoveAll<TEntity>(this ICosmosDatabaseContext entities) where TEntity : class
		{

		}
	}
}
