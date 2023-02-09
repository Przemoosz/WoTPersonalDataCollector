﻿namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
	using Microsoft.EntityFrameworkCore;
	using Dto;
	using Data;
	using Models;
	using Utilities;

	/// <inheritdoc cref="T:WotPersonalDataCollectorWebApp.CosmosDb.Context.ICosmosContext"/>
	public class CosmosDatabaseContext: DbContext, ICosmosDatabaseContext
    {
        private const string IdJson = "id";
        private const string BooleanTrue = "True";
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
            modelBuilder.Entity<VersionValidateResultModel>().ToContainer(_configuration.VersionModelContainerName);
            modelBuilder.Entity<VersionValidateResultModel>().HasPartitionKey(d => d.WasValidationCanceled);
            modelBuilder.Entity<VersionValidateResultModel>().Property(s => s.WasValidationCanceled)
	            .HasConversion(v => v.ToString(), v => ConvertToString(v));
            base.OnModelCreating(modelBuilder);
        }

        private bool ConvertToString(string s) => s.ToLower().Equals(BooleanTrue);

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
