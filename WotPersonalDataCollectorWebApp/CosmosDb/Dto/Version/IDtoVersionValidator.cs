﻿namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;

public interface IDtoVersionValidator
{
    void EnsureVersionCorrectness(WotDataCosmosDbDto wotDataCosmosDbDto);
}