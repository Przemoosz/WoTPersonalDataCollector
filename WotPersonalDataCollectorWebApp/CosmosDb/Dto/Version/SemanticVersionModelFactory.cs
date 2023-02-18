using WotPersonalDataCollector.WebApp.Exceptions;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version
{
	internal class SemanticVersionModelFactory : ISemanticVersionModelFactory
    {
        public SemanticVersionModel Create(string version)
        {
            var components = version.Split('.');

            if (components.Length != 3)
            {
                throw new DtoVersionComponentsException(
                    "Received DTO version from cosmosDb does not match Semantic Versioning format!");
            }
            int[] numericalComponents = new int[3];
            if (!int.TryParse(components[0], out numericalComponents[0]))
            {
                throw new DtoVersionComponentsException(
                    "Cannot parse Major version component to Int32!");
            }
            if (!int.TryParse(components[1], out numericalComponents[1]))
            {
                throw new DtoVersionComponentsException(
                    "Cannot parse Minor version component to Int32!");
            }
            if (!int.TryParse(components[2], out numericalComponents[2]))
            {
                throw new DtoVersionComponentsException(
                    "Cannot parse Patch version component to Int32!");
            }

            return new SemanticVersionModel(numericalComponents[0], numericalComponents[1], numericalComponents[2]);
        }
    }
}
