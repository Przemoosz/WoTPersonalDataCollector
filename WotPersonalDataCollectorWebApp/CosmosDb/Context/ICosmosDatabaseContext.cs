namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
    using Dto;
    using Microsoft.EntityFrameworkCore;
    public interface ICosmosDatabaseContext
    {
        DbSet<UserPersonalData> PersonalData { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}


