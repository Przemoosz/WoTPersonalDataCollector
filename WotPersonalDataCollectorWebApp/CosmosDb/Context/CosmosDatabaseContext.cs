using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.Data;
using WotPersonalDataCollectorWebApp.Utilities;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
    public class CosmosDatabaseContext: DbContext, ICosmosDatabaseContext
    {
        private const string IdJson = "id";
        private readonly IAspConfiguration _configuration = new AspConfiguration();
        public DbSet<UserPersonalData> PersonalData { get; set; }
        public CosmosDatabaseContext() : base()
        {
        }

        public CosmosDatabaseContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPersonalData>().ToContainer(_configuration.ContainerName);
            modelBuilder.Entity<UserPersonalData>().HasPartitionKey(d => d.AccountId);
            modelBuilder.Entity<UserPersonalData>().Property(d => d.Id).ToJsonProperty(IdJson);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(_configuration.CosmosConnectionString, _configuration.DatabaseName);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
