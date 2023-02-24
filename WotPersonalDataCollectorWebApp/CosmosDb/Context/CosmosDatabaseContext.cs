using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto;
using WotPersonalDataCollector.WebApp.Data;
using WotPersonalDataCollector.WebApp.Models;
using WotPersonalDataCollector.WebApp.Utilities;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Context
{
	/// <inheritdoc cref="T:WotPersonalDataCollector.WebApp.CosmosDb.Context.ICosmosContext"/>
	public class CosmosDatabaseContext: DbContext, ICosmosDatabaseContext
    {
        private const string IdJson = "id";
        private const string BooleanTrue = "true";
        private readonly IAspConfiguration _configuration = new AspConfiguration();
        public DbSet<WotDataCosmosDbDto> PersonalData { get; set; }
        public DbSet<VersionValidateResultModel> VersionValidateResult { get; set; }

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
            modelBuilder.Entity<VersionValidateResultModel>().Property(d => d.Id).ToJsonProperty(IdJson);
			modelBuilder.Entity<VersionValidateResultModel>().ToContainer(_configuration.VersionModelContainerName);
            modelBuilder.Entity<VersionValidateResultModel>().HasPartitionKey(d => d.WasValidationCanceled);
            modelBuilder.Entity<VersionValidateResultModel>().Property(s => s.WasValidationCanceled)
	            .HasConversion(v => v.ToString(), v => ConvertStringToBoolean(v));
            base.OnModelCreating(modelBuilder);
        }

        private bool ConvertStringToBoolean(string textToConvert) => textToConvert.ToLower().Equals(BooleanTrue);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(_configuration.CosmosConnectionString, _configuration.DatabaseName);
        }

        public Task<int> SaveChangesAsync()
        {
	        return base.SaveChangesAsync();
        }
    }
}
