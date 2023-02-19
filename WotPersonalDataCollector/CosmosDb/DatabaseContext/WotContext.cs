using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.CosmosDb.DatabaseContext
{
    internal sealed class WotContext : DbContext
    {
        private const string IdJson = "id";
        private readonly IConfiguration _configuration;
        public DbSet<WotDataCosmosDbDto> PersonalData { get; set; }
        public WotContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WotDataCosmosDbDto>().ToContainer(_configuration.WotDtoContainerName);
            modelBuilder.Entity<WotDataCosmosDbDto>().HasPartitionKey(d => d.AccountId);
            modelBuilder.Entity<WotDataCosmosDbDto>().Property(d => d.Id).ToJsonProperty(IdJson);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(_configuration.CosmosConnectionString, _configuration.DatabaseName);
        }
    }
}
