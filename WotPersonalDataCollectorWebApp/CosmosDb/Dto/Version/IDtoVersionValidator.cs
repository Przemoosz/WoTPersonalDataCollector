namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;

public interface IDtoVersionValidator
{
    void EnsureVersionCorrectness(UserPersonalData userPersonalData);
}