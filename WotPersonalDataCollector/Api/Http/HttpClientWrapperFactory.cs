namespace WotPersonalDataCollector.Api.Http
{
    internal class HttpClientWrapperFactory: IHttpClientWrapperFactory
    {
        private static IHttpClientWrapper _httpClient = null;
        public IHttpClientWrapper Create()
        {
            if (_httpClient is null)
            {
                _httpClient = new HttpClientWrapper();
            }
            return _httpClient;
        }
    }
}
