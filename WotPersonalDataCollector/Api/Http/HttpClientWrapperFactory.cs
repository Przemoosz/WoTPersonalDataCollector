namespace WotPersonalDataCollector.Api.Http
{
    internal class HttpClientWrapperFactory: IHttpClientWrapperFactory
    {
        private static IHttpClientWrapper _httpClient = null;
        public IHttpClientWrapper Create()
        {
            return _httpClient ??= new HttpClientWrapper();
        }
    }
}
