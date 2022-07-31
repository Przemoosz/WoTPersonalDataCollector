namespace WotPersonalDataCollector.Api.Http
{
    internal class HttpClientWrapperFactory: IHttpClientWrapperFactory
    {
        public IHttpClientWrapper Create()
        {
            return new HttpClientWrapper();
        }
    }
}
