namespace WotPersonalDataCollectorWebApp.Models;

public sealed class VersionValidateResultModel
{
    public string Id { get; init; }
    public int TotalItemsInCosmosDb { get; init; }
    public int CorrectVersionDtoCount { get; init; }
    public int WrongObjectsCount { get; init; }
    public int WrongVersionDtoCount { get; init; }
    public DateTime ValidationDate { get; init; }
    public bool WasValidationCanceled { get; init; }
}