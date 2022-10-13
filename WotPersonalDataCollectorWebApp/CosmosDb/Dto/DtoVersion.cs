namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    internal class DtoVersion: IEquatable<DtoVersion>
    {
        public int Major { get; init; }
        public int Minor { get; init; }
        public int Patch { get; init; }

        public DtoVersion(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public bool Equals(DtoVersion? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Major == other.Major && Minor == other.Minor && Patch == other.Patch;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DtoVersion)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Patch);
        }

        public static bool operator == (DtoVersion first, DtoVersion other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(DtoVersion first, DtoVersion other)
        {
            return !first.Equals(other);
        }
    }
}
