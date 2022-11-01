namespace WotPersonalDataCollectorWebApp.Exceptions
{
    public class DtoVersionException: Exception
    {
        public DtoVersionException()
        {
        }

        public DtoVersionException(string message): base(message)
        {
        }

        public DtoVersionException(string message, Exception innerException): base(message, innerException)
        {
        }
    }
}
