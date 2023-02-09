﻿namespace WotPersonalDataCollectorWebApp.CosmosDb.Context
{
	using Dto;
	using Models;
	using Microsoft.EntityFrameworkCore;
	
	/// <summary>
	/// Cosmos database context wrapper.
	/// </summary>
	public interface ICosmosContext
	{
		/// <summary>
		/// Gets container that contains <see cref="WotDataCosmosDbDto"/>.
		/// </summary>
		/// <value>
		/// <see cref="DbSet{TEntity}"/> of <see cref="WotDataCosmosDbDto"/>.
		/// </value>
		DbSet<WotDataCosmosDbDto> PersonalData { get; }

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync"/>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync"/>
		Task<int> SaveChangesAsync();

		/// <summary>
		/// Gets container that contains <see cref="VersionValidateResultModel"/>.
		/// </summary>
		/// <value>
		/// <see cref="DbSet{TEntity}"/> of <see cref="VersionValidateResultModel"/>.
		/// </value>
		DbSet<VersionValidateResultModel> VersionValidateResult { get; }
	}
}