namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    internal class DtoVersion
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
    }
}
