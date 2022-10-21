namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version
{
    internal class SemanticVersionModel : IEquatable<SemanticVersionModel>
    {
        public int Major { get; init; }
        public int Minor { get; init; }
        public int Patch { get; init; }

        public SemanticVersionModel(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public bool Equals(SemanticVersionModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Major == other.Major && Minor == other.Minor && Patch == other.Patch;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SemanticVersionModel)obj);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Patch);
        }

        public static bool operator ==(SemanticVersionModel first, SemanticVersionModel other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(SemanticVersionModel first, SemanticVersionModel other)
        {
            return !first.Equals(other);
        }
    }
}
