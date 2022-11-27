namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
    using Dto;
    using Microsoft.EntityFrameworkCore;
    using Models;
	public interface ICosmosDatabaseContext
    {
        DbSet<WotDataCosmosDbDto> PersonalData { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        DbSet<VersionValidateResultModel> VersionValidateResult { get;}
	}
}


