namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
    using Dto;
    using Microsoft.EntityFrameworkCore;
    public interface ICosmosDatabaseContext
    {
        DbSet<WotDataCosmosDbDto> PersonalData { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

    }
}


