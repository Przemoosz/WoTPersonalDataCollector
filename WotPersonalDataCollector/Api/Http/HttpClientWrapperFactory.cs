namespace WotPersonalDataCollector.Api.Http
{
    internal class HttpClientWrapperFactory: IHttpClientWrapperFactory
    {
        public HttpClientWrapper Create()
        {
            return new HttpClientWrapper();
        }
    }
}
