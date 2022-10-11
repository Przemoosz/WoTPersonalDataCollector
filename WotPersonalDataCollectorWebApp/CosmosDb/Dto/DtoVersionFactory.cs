using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    internal class DtoVersionFactory: IDtoVersionFactory
    {
        public DtoVersion Create(string version)
        {
            var components = version.Split('.');
            
            if (components.Length != 3)
            {
                throw new DtoVersionComponentsException(
                    "Received DTO version from cosmosDb does not match Semantic Versioning format!");
            }
            int[] numericalComponents = new int[3];
            if (!Int32.TryParse(components[0], out numericalComponents[0]))
            {
                throw new DtoVersionComponentsException(
                    "Cannot parse Major version component to Int32!");
            }
            if (!Int32.TryParse(components[1], out numericalComponents[1]))
            {
                throw new DtoVersionComponentsException(
                    "Cannot parse Minor version component to Int32!");
            }
            if (!Int32.TryParse(components[2], out numericalComponents[2]))
            {
                throw new DtoVersionComponentsException(
                    "Cannot parse Patch version component to Int32!");
            }

            return new DtoVersion(numericalComponents[0], numericalComponents[1], numericalComponents[2]);
        }
    }

    internal interface IDtoVersionFactory
    {
        DtoVersion Create(string version);
    }
}
