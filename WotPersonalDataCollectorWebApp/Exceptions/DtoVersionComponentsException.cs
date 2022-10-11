namespace WotPersonalDataCollectorWebApp.Exceptions
{
    internal class DtoVersionComponentsException: Exception
    {
        public DtoVersionComponentsException()
        {
        }

        public DtoVersionComponentsException(string message) : base(message)
        {
        }

        public DtoVersionComponentsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
