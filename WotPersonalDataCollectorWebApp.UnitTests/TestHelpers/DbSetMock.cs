using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace WotPersonalDataCollector.WebApp.UnitTests.TestHelpers
{
	internal static class DbSetMock
	{
		/// <summary>
		/// Converts <see cref="IEnumerable{T}"/> to <see cref="DbSet{TEntity}"/> that can be used to mock database context
		/// </summary>
		/// <typeparam name="T">Type of a data to mock DbSet</typeparam>
		/// <param name="fakeEnumerable">Data which will be converted to <see cref="DbSet{TEntity}"/></param>
		/// <returns><see cref="DbSet{TEntity}"/> which contains elements from <paramref name="fakeEnumerable"/></returns>
		public static DbSet<T> AsDbSet<T>(this IEnumerable<T> fakeEnumerable) where T : class
		{
			IQueryable<T> fakeQueryable = fakeEnumerable.AsQueryable();
			DbSet<T> fakeDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
			((IQueryable<T>)fakeDbSet).ElementType.Returns(fakeQueryable.ElementType);
			((IQueryable<T>)fakeDbSet).Expression.Returns(fakeQueryable.Expression);
			((IQueryable<T>)fakeDbSet).Provider.Returns(fakeQueryable.Provider);
			((IQueryable<T>)fakeDbSet).GetEnumerator().Returns(fakeQueryable.GetEnumerator());
			return fakeDbSet;
		}
	}
}
