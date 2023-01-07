namespace WotPersonalDataCollectorWebApp.Models;

public sealed class VersionValidateResultModel: IEquatable<VersionValidateResultModel>
{
    public string Id { get; init; }
    public int TotalItemsInCosmosDb { get; init; }
    public int CorrectVersionDtoCount { get; init; }
    public int WrongObjectsCount { get; init; }
    public int WrongVersionDtoCount { get; init; }
    public DateTime ValidationDate { get; init; }
    public bool WasValidationCanceled { get; init; }

    public bool Equals(VersionValidateResultModel other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return Id == other.Id && TotalItemsInCosmosDb == other.TotalItemsInCosmosDb &&
	           CorrectVersionDtoCount == other.CorrectVersionDtoCount && WrongObjectsCount == other.WrongObjectsCount &&
	           WrongVersionDtoCount == other.WrongVersionDtoCount && ValidationDate.Equals(other.ValidationDate) &&
	           WasValidationCanceled == other.WasValidationCanceled;
    }

    public override bool Equals(object obj)
    {
	    return ReferenceEquals(this, obj) || obj is VersionValidateResultModel other && Equals(other);
    }

    public override int GetHashCode()
    {
	    return HashCode.Combine(Id, TotalItemsInCosmosDb, CorrectVersionDtoCount, WrongObjectsCount,
		    WrongVersionDtoCount, ValidationDate, WasValidationCanceled);
    }
}