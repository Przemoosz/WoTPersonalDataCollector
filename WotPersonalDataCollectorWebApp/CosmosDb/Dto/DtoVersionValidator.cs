namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    using Utilities;
    internal class DtoVersionValidator: IDataVersionValidator
    {
        private readonly IAspConfiguration _aspConfiguration;
        private readonly IDtoVersionFactory _dtoVersionFactory;
        private readonly ILogger _logger;

        public DtoVersionValidator(IAspConfiguration aspConfiguration,IDtoVersionFactory dtoVersionFactory, ILogger logger)
        {
            _aspConfiguration = aspConfiguration;
            _dtoVersionFactory = dtoVersionFactory;
            _logger = logger;
        }

        public void EnsureVersionCorrectness(UserPersonalData userPersonalData)
        {
            var webDtoVersion = _dtoVersionFactory.Create(_aspConfiguration.WotDtoVersion);
            var cosmosDtoVersion = _dtoVersionFactory.Create(userPersonalData.ClassProperties.DtoVersion);

        }
        
    }

    public interface IDataVersionValidator
    {
        void EnsureVersionCorrectness(UserPersonalData userPersonalData);
    }
}
