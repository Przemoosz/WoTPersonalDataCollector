using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.Data;
using WotPersonalDataCollectorWebApp.Models;
using WotPersonalDataCollectorWebApp.Utilities;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
    public class CosmosDatabaseContext: DbContext, ICosmosDatabaseContext
    {
        private const string IdJson = "id";
        private readonly IAspConfiguration _configuration = new AspConfiguration();
        public DbSet<WotDataCosmosDbDto> PersonalData { get; set; }

        public CosmosDatabaseContext() : base()
        {
        }

        public CosmosDatabaseContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WotDataCosmosDbDto>().ToContainer(_configuration.WotDtoContainerName);
            modelBuilder.Entity<WotDataCosmosDbDto>().HasPartitionKey(d => d.AccountId);
            modelBuilder.Entity<WotDataCosmosDbDto>().Property(d => d.Id).ToJsonProperty(IdJson);
            modelBuilder.Entity<VersionValidateResultModel>().ToContainer("ds");
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(_configuration.CosmosConnectionString, _configuration.DatabaseName);
        }

        public Task<int> SaveChangesAsync()
        {
	        return base.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
