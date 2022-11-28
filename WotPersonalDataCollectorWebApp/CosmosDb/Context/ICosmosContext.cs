namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
	using Dto;
	using Models;
	using Microsoft.EntityFrameworkCore;
	
	public interface ICosmosContext
	{
		DbSet<WotDataCosmosDbDto> PersonalData { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		Task<int> SaveChangesAsync();
		DbSet<VersionValidateResultModel> VersionValidateResult { get; }
	}
}