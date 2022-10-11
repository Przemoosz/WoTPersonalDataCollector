namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    using Utilities;
    internal class DtoVersionValidator: IDataVersionValidator
    {
        private readonly IAspConfiguration _aspConfiguration;
        private readonly ILogger _logger;

        public DtoVersionValidator(IAspConfiguration aspConfiguration, ILogger logger)
        {
            _aspConfiguration = aspConfiguration;
            _logger = logger;
        }

        public void EnsureVersionCorrectness(UserPersonalData userPersonalData)
        {
            var webDtoVersion = _aspConfiguration.WotDtoVersion;

        }
        
    }

    public interface IDataVersionValidator
    {
    }
}
